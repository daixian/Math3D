#include "stdafx.h"

/*
 * 这个项目的DEBUG使用的是MD，然后生成调试信息（禁用优化）。
 * 但是需要注意，要去掉 _DEBUG 的预处理器定义，改为NDEBUG
*/

using namespace std;
using namespace xuexue;

int main()
{
    //使用一个
    cv::Ptr<cv::ShapeContextDistanceExtractor> mysc = cv::createShapeContextDistanceExtractor();

    vector<cv::Point> contQuery;
    for (size_t i = 0; i < 2; i++) {
        contQuery.push_back({0, 0});
        contQuery.push_back({6, 0});
        contQuery.push_back({6, -2});
        contQuery.push_back({-6, -2});
        contQuery.push_back({-6, 0});
    }
    vector<cv::Point> conti;
    for (size_t i = 0; i < 2; i++) {
        conti.push_back({0, 0});
        conti.push_back({6, 1});
        conti.push_back({6, -2});
        conti.push_back({-6, -2});
        conti.push_back({-6, 0});
    }
    try {
        //这个计算距离函数必须要9个点
        float dis = mysc->computeDistance(contQuery, conti);
        cout << dis;
    }
    catch (const std::exception& e) {
        cout << e.what();
    }

    vector<cv::Point> contii;
    for (size_t i = 0; i < 2; i++) {
        contii.push_back({0, 0});
        contii.push_back({12, 0});
        contii.push_back({12, -4});
        contii.push_back({-12, -4});
        contii.push_back({-12, 0});
        //contii.push_back({-6, 0});
        //contii.push_back({0, 0});
        //contii.push_back({6, 0});
        //contii.push_back({6, -2});
        //contii.push_back({-6, -2});
    }
    try {
        float dis = mysc->computeDistance(contQuery, contii);
        cout << dis;
    }
    catch (const std::exception& e) {
        cout << e.what();
    }

    return 0;
}