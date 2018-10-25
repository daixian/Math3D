#pragma once

namespace xuexue {

    class Math3D
    {
    public:
        Math3D();
        ~Math3D();

        #pragma region 公共的数学方法

        ///-------------------------------------------------------------------------------------------------
        /// <summary> 限定在-1,1的范围. </summary>
        ///
        /// <remarks> Dx, 2017/12/6. </remarks>
        ///
        /// <param name="a"> [in,out] A float to process. </param>
        ///
        /// <returns> A float. </returns>
        ///-------------------------------------------------------------------------------------------------
        static float limit1(float& a)
        {
            if (a > 0.99999999) {
                a = 0.99999999;
            }
            if (a < -0.99999999) {
                a = -0.99999999;
            }
            return a;
        }

        static double limit1(double& a)
        {
            if (a > 0.99999999) {
                a = 0.99999999;
            }
            if (a < -0.99999999) {
                a = -0.99999999;
            }
            return a;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> 判定两个float是否相等. </summary>
        ///
        /// <remarks> Dx, 2017/8/9. </remarks>
        ///
        /// <param name="f1"> The first float. </param>
        /// <param name="f2"> The second float. </param>
        ///
        /// <returns>
        /// True if it succeeds, false if it fails.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------
        static bool Equal(float f1, float f2)
        {
            return (abs(f1 - f2) < 1e-4f);
        }

        #pragma endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary> xyz旋转顺序的欧拉角（弧度）转换到一个旋转矩阵. </summary>
        ///
        /// <remarks> Xian Dai, 2017/5/3. </remarks>
        ///
        /// <param name="theta"> [in] 欧拉角（弧度）. </param>
        ///
        /// <returns> A Mat. </returns>
        ///-------------------------------------------------------------------------------------------------
        static cv::Mat eulerAnglesToRotationMatrix(const cv::Vec3f& theta);
    };
}