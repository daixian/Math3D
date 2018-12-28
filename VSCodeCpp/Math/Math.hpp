#include "stdio.h"

class Math
{
  private:
    /* data */
  public:
    Math(/* args */);
    ~Math();

    int getValue()
    {
        return 123;
    }
};

Math::Math(/* args */)
{
}

Math::~Math()
{
}

typedef Math dxMath;