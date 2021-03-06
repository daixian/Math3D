// Math3D.cpp : 定义 DLL 应用程序的导出函数。
//
#include "stdafx.h"

namespace xuexue {

    Math3D::Math3D()
    {
    }

    Math3D::~Math3D()
    {
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary> xyz旋转顺序的欧拉角（弧度）转换到一个旋转矩阵. </summary>
    ///
    /// <remarks> Xian Dai, 2017/5/3. </remarks>
    ///
    /// <param name="theta"> [in] 欧拉角（弧度）. </param>
    ///
    /// <returns> A Mat. </returns>
    ///-------------------------------------------------------------------------------------------------
    cv::Mat Math3D::eulerAnglesToRotationMatrix(const cv::Vec3f& theta)
    {
        //这几个矩阵就是用来点在右边乘的
        // Calculate rotation about x axis
        cv::Mat R_x = (cv::Mat_<double>(3, 3) <<
                       1, 0, 0,
                       0, cos(theta[0]), -sin(theta[0]),
                       0, sin(theta[0]), cos(theta[0])
                      );

        // Calculate rotation about y axis
        cv::Mat R_y = (cv::Mat_<double>(3, 3) <<
                       cos(theta[1]), 0, sin(theta[1]),
                       0, 1, 0,
                       -sin(theta[1]), 0, cos(theta[1])
                      );

        // Calculate rotation about z axis
        cv::Mat R_z = (cv::Mat_<double>(3, 3) <<
                       cos(theta[2]), -sin(theta[2]), 0,
                       sin(theta[2]), cos(theta[2]), 0,
                       0, 0, 1);

        // Combined rotation matrix
        cv::Mat R = R_z * R_y * R_x;
        return R;
    }
}