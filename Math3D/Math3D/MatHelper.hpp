#include "opencv2/opencv.hpp"

namespace dxlib {

class MatHelper
{
  public:
    MatHelper() {}
    ~MatHelper() {}

    void test()
    {
        cv::Mat sbiamge = cv::Mat(1080, 1920, CV_8UC3, cv::Scalar(0, 0, 0)); //全部画上黑色;

        for (size_t x = 0; x < 1920; x++) {
            if (x % 3 == 0) {
                for (size_t y = 0; y < 1080; y++) {
                    uchar* data = sbiamge.ptr<uchar>(y, x); //得到(x,y)除的像素起始指针
                    data[0] = 255;
                    data[1] = 255;
                    data[2] = 255;
                }
            }
        }
        cv::imwrite("sbscreen-2d.png", sbiamge);

        // 构造一个VideoWriter
        cv::VideoWriter video("sbscreen-2d.avi", CV_FOURCC('M', 'J', 'P', 'G'), 25.0, cv::Size(1920, 1080));
        for (size_t i = 0; i < 250; i++) {
            video << sbiamge;
        }
        video.release();
    }

  private:
};
} // namespace dxlib
