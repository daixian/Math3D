#include "stdafx.h"
#include "UnityCamera.h"

/*
 * 这个项目的DEBUG使用的是MD，然后生成调试信息（禁用优化）。
 * 但是需要注意，要去掉 _DEBUG 的预处理器定义，改为NDEBUG
*/

using namespace std;
using namespace xuexue;

using namespace cv;
//参数分别为输入图像，输出图像，压缩比例
void SVDRESTRUCT(const cv::Mat& inputImg, cv::Mat& outputImg, double theratio)
{
    cv::Mat tempt;
    cv::Mat U, W, V;
    inputImg.convertTo(tempt, CV_32FC1);
    cv::SVD::compute(tempt, W, U, V);
    cv::Mat w = Mat::zeros(Size(W.rows, W.rows), CV_32FC1);
    int len = theratio * W.rows;
    for (int i = 0; i < len; ++i)
        w.ptr<float>(i)[i] = W.ptr<float>(i)[0];
    cv::Mat result = U * w * V;
    result.convertTo(outputImg, CV_8UC1);
}

///-------------------------------------------------------------------------------------------------
/// <summary> 立体相机求坐标,d = pL.y - pR.y. </summary>
///
/// <remarks> Dx, 2019/1/14. </remarks>
///
/// <param name="Q">   Q矩阵. </param>
/// <param name="pLx"> L相机的点坐标x. </param>
/// <param name="pLy"> L相机的点坐标y. </param>
/// <param name="d">   pL.y - pR.y. </param>
///
/// <returns> An Eigen::Vector3d. </returns>
///-------------------------------------------------------------------------------------------------
static inline Eigen::Vector3d stereoPos(const cv::Mat& Q, double pLx, double pLy, double d)
{
    //首先考虑d,如果d = pL.y - pR.y反过来变成pR.y - pL.y,那么上下也会颠倒,变化比较大,所以考虑是使用pL.y - pR.y
    //考虑x,y坐标,考虑使用x,y去减去相机m矩阵里的中心位置,但是实验出来效果很乱,可能不正确.
    //(未能解决这个标定结果点搞成弧形面的问题,点基本不成形)
    cv::Mat srcp = (cv::Mat_<double>(4, 1) << pLx, pLy, d, 1);
    cv::Mat xyzw = Q * srcp;
    double* _r = xyzw.ptr<double>();
    Eigen::Vector3d point(_r[0] / _r[3], _r[1] / _r[3], -_r[2] / _r[3]); //3d坐标
    //上面的3d坐标需要把z值取反过来,物体的远近数据才是正确的.所以考虑以后都取反的z值
    return point;
}

int main()
{
    dxlib::UnityCamera cam;
    cam.position = {0, 0, 0};
    cam.setEulerAngle(0, 0, 0);
    cam.updateW2C();
    cam.updateProj();
    Eigen::Vector2d screenPoint;
    bool success = cam.point2Screen({-4, 5, 10}, screenPoint);

    cv::Mat scrX = imread("C:\\Users\\dx\\OneDrive\\Pictures\\large.jpg", 0);

    Mat projL = (cv::Mat_<double>(3, 4) << 9.3276103922797446e+01, 0., 3.9422296142578125e+02, 0.,
                 0., 9.3276103922797446e+01, 2.5522330379486084e+02, 0.,
                 0., 0., 1., 0.);

    Mat projR = (cv::Mat_<double>(3, 4) << 9.3276103922797446e+01, 0., 3.6333302307128906e+02,
                 -1.5729965089521910e+01, 0., 9.3276103922797446e+01,
                 2.5522330379486084e+02, 0., 0., 0., 1., 0.);

    Mat Q = (cv::Mat_<double>(4, 4) << 1., 0., 0., -3.9422296142578125e+02, 0., 1., 0.,
             -2.5522330379486084e+02, 0., 0., 0., 9.3276103922797446e+01, 0.,
             0., 5.9298354059876965e+00, -1.8317225014324509e+02);

    Eigen::Vector3d Point3d = stereoPos(Q, 100, 200, 50);

    vector<Point3f> vp;
    vp.push_back(Point3f(100, 200, 1));
    vector<Point3f> vpout;

    Mat obj_corners(4, 1, CV_32FC2);
    Mat scene_corners(4, 1, CV_32FC2);

    try {

        cv::perspectiveTransform(vp, vpout, Q); //这个重投影如果是4x4的矩阵那么表示把一个vector3重投影到另一个vector3
    }
    catch (const std::exception& e) {
        cout << e.what() << endl;
    }

    //cv::triangulatePoints(projL,projR,)

    cv::Mat result;
    SVDRESTRUCT(scrX, result, 0.5);
    cv::imshow("1", scrX);
    waitKey(0);
}