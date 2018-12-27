# This file will be configured to contain variables for CPack. These variables
# should be set in the CMake list file of the project before CPack module is
# included. The list of available CPACK_xxx variables and their associated
# documentation may be obtained using
#  cpack --help-variable-list
#
# Some variables are common to all generators (e.g. CPACK_PACKAGE_NAME)
# and some are specific to a generator
# (e.g. CPACK_NSIS_EXTRA_INSTALL_COMMANDS). The generator specific variables
# usually begin with CPACK_<GENNAME>_xxxx.


SET(CPACK_BINARY_7Z "OFF")
SET(CPACK_BINARY_BUNDLE "")
SET(CPACK_BINARY_CYGWIN "")
SET(CPACK_BINARY_DEB "")
SET(CPACK_BINARY_DRAGNDROP "")
SET(CPACK_BINARY_FREEBSD "")
SET(CPACK_BINARY_IFW "OFF")
SET(CPACK_BINARY_NSIS "ON")
SET(CPACK_BINARY_OSXX11 "")
SET(CPACK_BINARY_PACKAGEMAKER "")
SET(CPACK_BINARY_PRODUCTBUILD "")
SET(CPACK_BINARY_RPM "")
SET(CPACK_BINARY_STGZ "")
SET(CPACK_BINARY_TBZ2 "")
SET(CPACK_BINARY_TGZ "")
SET(CPACK_BINARY_TXZ "")
SET(CPACK_BINARY_TZ "")
SET(CPACK_BINARY_WIX "OFF")
SET(CPACK_BINARY_ZIP "OFF")
SET(CPACK_BUILD_SOURCE_DIRS "d:/temp/VSCodeCpp;d:/temp/VSCodeCpp/build")
SET(CPACK_CMAKE_GENERATOR "Ninja")
SET(CPACK_COMPONENTS_ALL "")
SET(CPACK_COMPONENT_UNSPECIFIED_HIDDEN "TRUE")
SET(CPACK_COMPONENT_UNSPECIFIED_REQUIRED "TRUE")
SET(CPACK_GENERATOR "7Z;ZIP")
SET(CPACK_IGNORE_FILES "/CVS/;/\\.svn/;/\\.bzr/;/\\.hg/;/\\.git/;\\.swp\$;\\.#;/#")
SET(CPACK_INSTALLED_DIRECTORIES "d:/temp/VSCodeCpp;/")
SET(CPACK_INSTALL_CMAKE_PROJECTS "")
SET(CPACK_INSTALL_PREFIX "C:/Program Files (x86)/test1")
SET(CPACK_MODULE_PATH "")
SET(CPACK_NSIS_DISPLAY_NAME "test1 0.1.1")
SET(CPACK_NSIS_INSTALLER_ICON_CODE "")
SET(CPACK_NSIS_INSTALLER_MUI_ICON_CODE "")
SET(CPACK_NSIS_INSTALL_ROOT "$PROGRAMFILES64")
SET(CPACK_NSIS_PACKAGE_NAME "test1 0.1.1")
SET(CPACK_OUTPUT_CONFIG_FILE "d:/temp/VSCodeCpp/build/CPackConfig.cmake")
SET(CPACK_PACKAGE_DEFAULT_LOCATION "/")
SET(CPACK_PACKAGE_DESCRIPTION_FILE "D:/soft/cmake-3.10.0-rc4-win64-x64/share/cmake-3.10/Templates/CPack.GenericDescription.txt")
SET(CPACK_PACKAGE_DESCRIPTION_SUMMARY "test1 built using CMake")
SET(CPACK_PACKAGE_FILE_NAME "test1-0.1.1-Source")
SET(CPACK_PACKAGE_INSTALL_DIRECTORY "test1 0.1.1")
SET(CPACK_PACKAGE_INSTALL_REGISTRY_KEY "test1 0.1.1")
SET(CPACK_PACKAGE_NAME "test1")
SET(CPACK_PACKAGE_RELOCATABLE "true")
SET(CPACK_PACKAGE_VENDOR "Humanity")
SET(CPACK_PACKAGE_VERSION "0.1.1")
SET(CPACK_PACKAGE_VERSION_MAJOR "0")
SET(CPACK_PACKAGE_VERSION_MINOR "1")
SET(CPACK_PACKAGE_VERSION_PATCH "1")
SET(CPACK_PROJECT_NAME "test1")
SET(CPACK_PROJECT_VERSION "0.1.0")
SET(CPACK_RESOURCE_FILE_LICENSE "D:/soft/cmake-3.10.0-rc4-win64-x64/share/cmake-3.10/Templates/CPack.GenericLicense.txt")
SET(CPACK_RESOURCE_FILE_README "D:/soft/cmake-3.10.0-rc4-win64-x64/share/cmake-3.10/Templates/CPack.GenericDescription.txt")
SET(CPACK_RESOURCE_FILE_WELCOME "D:/soft/cmake-3.10.0-rc4-win64-x64/share/cmake-3.10/Templates/CPack.GenericWelcome.txt")
SET(CPACK_RPM_PACKAGE_SOURCES "ON")
SET(CPACK_SET_DESTDIR "OFF")
SET(CPACK_SOURCE_7Z "ON")
SET(CPACK_SOURCE_CYGWIN "")
SET(CPACK_SOURCE_GENERATOR "7Z;ZIP")
SET(CPACK_SOURCE_IGNORE_FILES "/CVS/;/\\.svn/;/\\.bzr/;/\\.hg/;/\\.git/;\\.swp\$;\\.#;/#")
SET(CPACK_SOURCE_INSTALLED_DIRECTORIES "d:/temp/VSCodeCpp;/")
SET(CPACK_SOURCE_OUTPUT_CONFIG_FILE "d:/temp/VSCodeCpp/build/CPackSourceConfig.cmake")
SET(CPACK_SOURCE_PACKAGE_FILE_NAME "test1-0.1.1-Source")
SET(CPACK_SOURCE_RPM "")
SET(CPACK_SOURCE_TBZ2 "")
SET(CPACK_SOURCE_TGZ "")
SET(CPACK_SOURCE_TOPLEVEL_TAG "win64-Source")
SET(CPACK_SOURCE_TXZ "")
SET(CPACK_SOURCE_TZ "")
SET(CPACK_SOURCE_ZIP "ON")
SET(CPACK_STRIP_FILES "")
SET(CPACK_SYSTEM_NAME "win64")
SET(CPACK_TOPLEVEL_TAG "win64-Source")
SET(CPACK_WIX_SIZEOF_VOID_P "8")

if(NOT CPACK_PROPERTIES_FILE)
  set(CPACK_PROPERTIES_FILE "d:/temp/VSCodeCpp/build/CPackProperties.cmake")
endif()

if(EXISTS ${CPACK_PROPERTIES_FILE})
  include(${CPACK_PROPERTIES_FILE})
endif()
