#include "stdafx.h"

/*
 * 这个项目的DEBUG使用的是MD，然后生成调试信息（禁用优化）。
 * 但是需要注意，要去掉 _DEBUG 的预处理器定义，改为NDEBUG
*/

using namespace std;
using namespace xuexue;

#define MATRIX_SIZE 50

struct TestSTruct
{
    Eigen::Vector3f a;
    Eigen::Vector3f b;
    Eigen::Vector3f c;
};

//熟悉一下Eigen库的类型
int main()
{
    TestSTruct tes;

    cout << "*************************** 验证EulerAngles ***************************" << endl;
    //这是没有旋转位移，处于原点的东西，缩放为1
    cv::Mat mat_4x4 = (cv::Mat_<double>(4, 4) <<
                       1.00000, 0.00000, 0.00000, 0.00000,
                       0.00000,	1.00000, 0.00000, 0.00000,
                       0.00000,	0.00000, 1.00000, 0.00000,
                       0.00000,	0.00000, 0.00000, 1.00000 );
    //但是它的旋转矩阵应该为
    cv::Mat mat_r_33 = (cv::Mat_<double>(3, 3) <<
                        0, 0, 0,
                        0, 0, 0,
                        0, 0, 0);

    mat_r_33 = Math3D::eulerAnglesToRotationMatrix(cv::Vec3f(M_PI / 2, M_PI / 3, M_PI / 4));
    cout << mat_r_33 << endl;

    Eigen::EulerAngles < double, Eigen::EulerSystem < Eigen::EULER_Z, Eigen::EULER_Y, Eigen::EULER_X >> euler(M_PI / 4, M_PI / 3, M_PI / 2);
    Eigen::EulerAnglesZYXd euler2(M_PI / 4, M_PI / 3, M_PI / 2);//和上面写法等效
    cout << euler.toRotationMatrix() << endl;
    cout << euler2.toRotationMatrix() << endl;

    cout << "*************************** 验证Rodrigues ***************************" << endl;
    mat_r_33 = Math3D::eulerAnglesToRotationMatrix(cv::Vec3f(1, 0, 0));//沿x轴转1弧度
    cv::Mat rDstv3, rDstm33;
    cv::Rodrigues(mat_r_33, rDstv3);
    cout << rDstv3 << endl;//这里计算结果是 [1.000000009487307;0; 0]

    cv::Rodrigues(rDstv3, rDstm33);//倒回来计算一次
    cout << rDstm33 << endl;//倒回来的计算也正确

    cout << "*************************** 验证cv2eigen和四元数 ***************************" << endl;
    Eigen::Matrix3d mat_r_e;
    cv::cv2eigen(mat_r_33, mat_r_e);
    cout << mat_r_33 << endl;
    cout << mat_r_e << endl;

    Eigen::Quaterniond q(mat_r_e);
    cout << q.vec() << "," << q.w() << endl;//这个确实是1弧度的四元数

    cout << "*************************** 标定出来的旋转 ***************************" << endl;
    cv::FileStorage fs("extrinsics.yml", cv::FileStorage::READ);
    cv::Mat R;
    Eigen::Matrix3d R_e;
    fs["R"] >> R;
    cv::cv2eigen(R, R_e);
    Eigen::EulerAnglesYXZd eulerR(R_e);//untiy是ZXY，YXZ
    cout << eulerR.angles() * 57.3 << endl;//弧度转角度
    fs.release();

    //前三个参数为：数据类型、行、列。 声明一个2x3的float矩阵
    Eigen::Matrix<float, 2, 3> matrix_23;

    Eigen::Vector3d v_3d;

    Eigen::Matrix3d matrix_33 = Eigen::Matrix3d::Zero();//初始化为0

    //如果不确定矩阵大小，那么设置为动态大小的矩阵，实际上是一个-1
    Eigen::Matrix<double, Eigen::Dynamic, Eigen::Dynamic> matrix_dynamic;

    Eigen::MatrixXd matrix_x;//这也就是上面的动态大小的定义

    //对矩阵的操作
    matrix_23 << 1, 2, 3, 4, 5, 6;

    //用()访问矩阵中的元素
    for (size_t i = 0; i < 1; i++) {
        for (size_t j = 0; j < 2; j++) {
            cout << matrix_23(i, j) << endl;
        }
    }
    v_3d << 1;//它等于1和两个随机数

    v_3d << 3, 2, 1;//会覆盖上面的1

    //v_3d << 3, 2, 1, 0, 3, 2, 1, 0;//这样写是错误的编译不会报错

    // 矩阵和向量相乘（实际上仍是矩阵和矩阵）
    // 但是在这里你不能混合两种不同类型的矩阵，像这样是错的
    //Eigen::Matrix<double, 2, 1> result_wrong_type = matrix_23 * v_3d;//float和double类型

    //应该显式转换
    Eigen::Matrix<double, 2, 1> result = matrix_23.cast<double>() * v_3d;

    //同样不能搞错矩阵的维度
    //Eigen::Matrix<double, 2, 3> result_wrong_dimension = matrix_23.cast<double>() * v_3d;

    //一些矩阵运算
    matrix_33 = Eigen::Matrix3d::Random();

    cout << matrix_33 << endl << endl;

    cout << matrix_33.transpose() << endl;//转置
    cout << matrix_33.sum() << endl;//各元素和
    cout << matrix_33.trace() << endl;//迹
    cout << 10 * matrix_33 << endl; //数乘
    cout << matrix_33.inverse() << endl;//逆
    cout << matrix_33.determinant() << endl;//行列式

    //特征值
    //实对称矩阵可以保证对角化成功
    Eigen::SelfAdjointEigenSolver<Eigen::Matrix3d> eigen_solver(matrix_33.transpose() * matrix_33);
    eigen_solver.eigenvalues();
    eigen_solver.eigenvectors();


}