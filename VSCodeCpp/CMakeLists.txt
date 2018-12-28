cmake_minimum_required(VERSION 3.0.0)
project(cmakestudy VERSION 0.1.0)

include_directories ("D:/SDK/eigen/eigen")

#指定子目录
include_directories ("${PROJECT_SOURCE_DIR}/Math")
add_subdirectory("${PROJECT_SOURCE_DIR}/Math")

include(CTest)
enable_testing()

add_executable(cmakestudy main.cpp)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
