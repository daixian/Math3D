#include "stdafx.h"

/*
 * �����Ŀ��DEBUGʹ�õ���MD��Ȼ�����ɵ�����Ϣ�������Ż�����
 * ������Ҫע�⣬Ҫȥ�� _DEBUG ��Ԥ���������壬��ΪNDEBUG
*/

using namespace std;
using namespace xuexue;

int main_sct()
{
    //ʹ��һ��
    cv::Ptr<cv::ShapeContextDistanceExtractor> mysc = cv::createShapeContextDistanceExtractor();

    vector<cv::Point2f> contQuery;
    for (size_t i = 0; i < 4; i++) {
        contQuery.push_back({0, 0});
        contQuery.push_back({8, 0});
        contQuery.push_back({0, 16});
        contQuery.push_back({0, 0});
    }
    vector<cv::Point> conti; //�������ʵ����������
    for (size_t i = 0; i < 4; i++) {
        conti.push_back({1, 0});
        conti.push_back({9, 0});
        conti.push_back({1, 16});
        conti.push_back({1, 0});
    }
    try {
        vector<cv::Point> cont3;
        cont3.push_back({0, 0});
        cont3.push_back({8, 0});
        cont3.push_back({0, 16});
        double len = cv::arcLength(cont3, false); //����������������,�����8+8������5
        len = cv::arcLength(cont3, true);         //����Ǽ���16���ȵ�
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