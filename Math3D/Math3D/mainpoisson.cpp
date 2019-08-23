// Copyright (c) 2018 Martyn Afford
// Licensed under the MIT licence
#include "stdafx.h"
#include <chrono>
#include <cstdlib>
#include <iostream>
#include <random>
#include "poisson_disc_distribution.hpp"

int main()
{
    constexpr auto width = 100;
    constexpr auto height = 100;

    char map[height][width] = {};

    auto seed = std::chrono::system_clock::now().time_since_epoch().count();

    std::default_random_engine engine{(unsigned int)seed};
    std::uniform_real_distribution<float> distribution{0, 1};

    bridson::config conf;
    conf.width = width;
    conf.height = height;
    conf.min_distance = 0.5f;

    std::vector<bridson::point> result;

    clock_t t0 = clock();

    bridson::poisson_disc_distribution(
        conf,
        // random
        [&engine, &distribution](float range) {
            return distribution(engine) * range;
        },
        // in_area
        [&](bridson::point p) {
            return p.x > 0 && p.x < width && p.y > 0 && p.y < height;
        },
        // output
        [&map, &result](bridson::point p) {
            //map[static_cast<int>(p.y)][static_cast<int>(p.x)] = '.';
            result.push_back(p);
        });

    double costTime = ((double)(clock() - t0)) / CLOCKS_PER_SEC;
    //for (auto& row : map) {
    //    for (auto& cell : row) {
    //        std::cout << (cell ? cell : ' ');
    //    }

    //    std::cout << '\n';
    //}

    cv::Mat iamge = cv::Mat(2048, 2048, CV_8UC3, cv::Scalar(0, 0, 0)); //全部画上黑色;

    for (size_t i = 0; i < result.size(); i++) {
        cv::Point p((int)(result[i].x * 20.48), (int)(result[i].y * 20.48));
        cv::circle(iamge, p, conf.min_distance * 20.48 / 2, cv::Scalar(255, 255, 255), -1);
    }
    cv::imwrite("result.png", iamge);
    std::cout << "costTime=" << costTime << "\n";
    return EXIT_SUCCESS;
}
