#include "stdafx.h"

/*
 * 这个项目的DEBUG使用的是MD，然后生成调试信息（禁用优化）。
 * 但是需要注意，要去掉 _DEBUG 的预处理器定义，改为NDEBUG
*/

using namespace std;

#define MATRIX_SIZE 50

//熟悉一下Eigen库的类型
int main()
{
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

    v_3d << 3, 2, 1;
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