#include <iostream>
#include "./Math/Math.hpp"

int main(int, char **)
{

    for (size_t i = 0; i < 100; i++)
    {
        std::cout << i << std::endl;
    }
    
    Math m;
    std::cout << m.getValue();//改变颜色

    std::cout << "Hello, world!\n";
}
