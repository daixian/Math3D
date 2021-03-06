// stdafx.h: 标准系统包含文件的包含文件，
// 或是经常使用但不常更改的
// 项目特定的包含文件
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // 从 Windows 头文件中排除极少使用的内容
// Windows 头文件
#include <windows.h>



// 在此处引用程序需要的其他标头

#include <iostream>
#include <ctime>

#define _USE_MATH_DEFINES //定义了之后才有M_PI的宏定义
#include <math.h>

//Eigen 部分
#include <Eigen/Core>
//稠密矩阵的代数运算（逆、特征值等）
#include <Eigen/Dense>
#include <Eigen/Geometry>
#include <unsupported/Eigen/EulerAngles>

#define HAVE_OPENCV_VIDEOIO
#define HAVE_OPENCV_VIDEO
#define HAVE_OPENCV_IMGCODECS
#define HAVE_OPENCV_HIGHGUI
#define HAVE_OPENCV_CALIB3D
#define HAVE_OPENCV_FEATURES2D
#include "opencv2/opencv.hpp"
#include <opencv2/core/eigen.hpp>

#include "Math3D.h"