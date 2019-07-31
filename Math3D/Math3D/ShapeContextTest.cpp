#include "stdafx.h"

/*
 * �����Ŀ��DEBUGʹ�õ���MD��Ȼ�����ɵ�����Ϣ�������Ż�����
 * ������Ҫע�⣬Ҫȥ�� _DEBUG ��Ԥ���������壬��ΪNDEBUG
*/

using namespace std;
using namespace xuexue;

int main()
{
    //ʹ��һ��
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
        //���������뺯������Ҫ9����
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