#pragma once

//Eigen 部分
#include <Eigen/Core>
//稠密矩阵的代数运算（逆、特征值等）
#include <Eigen/Dense>
#include <Eigen/Geometry>
#include <unsupported/Eigen/EulerAngles>

#define _USE_MATH_DEFINES //定义了之后才有M_PI的宏定义
#include <math.h>

namespace dxlib {

///-------------------------------------------------------------------------------------------------
/// <summary> 一个untiy3d里面相机的计算. </summary>
///
/// <remarks> Dx, 2019/8/15. </remarks>
///-------------------------------------------------------------------------------------------------
class UnityCamera
{
  public:
    UnityCamera()
    {
        updateW2C();
        updateProj();
    }
    ~UnityCamera()
    {
    }

    // 相机的世界坐标.
    Eigen::Vector3d position = {0, 0, 0};

    // 相机的世界旋转.
    Eigen::Quaterniond rotation = {0, 0, 0, 1};

    // 相机FOV(度数).
    double FOV = 60;

    // 近裁剪面.
    double Near = 0.01;

    // 远裁剪面.
    double Far = 100;

    // 屏幕宽.
    int screenWidth = 1920;

    // 屏幕高.
    int screenHeight = 1080;

    // 世界到相机矩阵.
    Eigen::Matrix<double, 4, 4> worldToCameraMatrix;

    // 投影矩阵
    Eigen::Matrix<double, 4, 4> projectionMatrix;

    ///-------------------------------------------------------------------------------------------------
    /// <summary> 设置欧拉角(度数). </summary>
    ///
    /// <remarks> Dx, 2019/8/15. </remarks>
    ///
    /// <param name="x"> The x. </param>
    /// <param name="y"> The y. </param>
    /// <param name="z"> The z. </param>
    ///-------------------------------------------------------------------------------------------------
    void setEulerAngle(double x, double y, double z)
    {
        //外坐标轴旋转即是内轴旋转反过来,Eigen中是相对自身坐标系，而unity中是相对外部坐标系
        Eigen::EulerAnglesYXZd eulerR(y / 180 * M_PI, x / 180 * M_PI, z / 180 * M_PI); //untiy是ZXY所以反过来用YXZ
        //这里不直接调用angles(),直接作为四元数的构造
        rotation = Eigen::Quaterniond(eulerR);
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary> 根据坐标和旋转计算更新worldToCameraMatrix矩阵. </summary>
    ///
    /// <remarks> Dx, 2019/8/15. </remarks>
    ///-------------------------------------------------------------------------------------------------
    void updateW2C()
    {
        Eigen::Matrix3d e = rotation.toRotationMatrix();

        //构造相机空间到世界空间的矩阵
        Eigen::Matrix<double, 4, 4> c2w;
        c2w << e(0, 0), e(0, 1), e(0, 2), position[0],
            e(1, 0), e(1, 1), e(1, 2), position[1],
            e(2, 0), e(2, 1), e(2, 2), position[2],
            0.0, 0.0, 0.0, 1.0;

        //相机空间的坐标系z反过来了,所以要叠加一个-z的矩阵
        Eigen::Matrix<double, 4, 4> mz;
        mz << 1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, -1, 0,
            0, 0, 0, 1;

        worldToCameraMatrix = mz * c2w.inverse(); //求逆然后叠加,这里mz只能在*号左边
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary> 根据FOV和纵横比计算投影矩阵. </summary>
    ///
    /// <remarks> Dx, 2019/8/15. </remarks>
    ///-------------------------------------------------------------------------------------------------
    void updateProj()
    {
        //这里横纵比实际上还是需要一个Viewport Rect中的W和H属性共同决定
        double aspect = screenWidth / (double)screenHeight;

        //公式推导见冯乐乐的Shader书P79,第一个公式M_frustum,图2有笔误
        projectionMatrix << (1 / tan((FOV / 180 * M_PI) / 2)) / aspect, 0, 0, 0,
            0, 1 / tan((FOV / 180 * M_PI) / 2), 0, 0,
            0, 0, -(Far + Near) / (Far - Near), -2 * Near * Far / (Far - Near),
            0, 0, -1, 0;
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary> 3D空间中的一点映射到屏幕上的点的结果,如果不在视野范围内则返回false. </summary>
    ///
    /// <remarks> Dx, 2019/8/15. </remarks>
    ///
    /// <param name="p3">	   The third Eigen::Vector3d. </param>
    /// <param name="pScreen"> [out] The screen. </param>
    ///
    /// <returns> 是否在视野范围内,不在视野范围内的点计算可能会错误nan. </returns>
    ///-------------------------------------------------------------------------------------------------
    bool point2Screen(const Eigen::Vector3d& p3, Eigen::Vector2d& pScreen)
    {
        Eigen::Matrix<double, 4, 1> hp3, p_clip;
        hp3 << p3.x(), p3.y(), p3.z(), 1; //齐次

        p_clip = projectionMatrix * worldToCameraMatrix * hp3;
        //p_clip的范围在unity里是[-1,1]
        if (p_clip(0) >= -abs(p_clip(3)) &&
            p_clip(0) <= abs(p_clip(3)) &&
            p_clip(1) >= -abs(p_clip(3)) &&
            p_clip(1) <= abs(p_clip(3)) &&
            p_clip(2) >= -abs(p_clip(3)) &&
            p_clip(2) <= abs(p_clip(3))) {
            //把[-1,1]的范围转化到以屏幕左上角为原点的像素坐标
            double x = p_clip(0) * screenWidth / (2 * p_clip(3)) + screenWidth / 2;
            double y = screenHeight / 2 - p_clip(1) / p_clip(3) * screenHeight / 2;
            pScreen = Eigen::Vector2d(x, y);
            return true;
        }
        else {
            return false;
        }
    }

  private:
    //无用的函数,初始状态的正确值应该如下
    void setMat()
    {
        worldToCameraMatrix << 1.0, 0.0, 0.0, 0.0,
            0.0, 1.0, 0.0, 0.0,
            0.0, 0.0, -1.0, 0.0,
            0.0, 0.0, 0.0, 1.0;

        projectionMatrix << 0.974278569221497, 0.0, 0.0, 0.0,
            0.0, 1.73205077648163, 0.0, 0.0,
            0.0, 0.0, -1.00020003318787, -0.0200020000338554,
            0.0, 0.0, -1.0, 0.0;
    }
};
} // namespace dxlib