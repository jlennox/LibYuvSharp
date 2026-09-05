set(JPEG_VERSION 3.2.0)
set(ARTIFACTS "${CMAKE_CURRENT_LIST_DIR}/../artifacts")
file(MAKE_DIRECTORY "${ARTIFACTS}")
file(DOWNLOAD
  "https://github.com/libjpeg-turbo/libjpeg-turbo/releases/download/${JPEG_VERSION}/libjpeg-turbo-${JPEG_VERSION}.tar.gz"
  "${ARTIFACTS}/libjpeg-turbo-${JPEG_VERSION}.tar.gz"
  EXPECTED_HASH SHA256=6f30092cef9fb839779646608f4ee14ae3cbac989c47fa05e841b0841f09878e
  TLS_VERIFY ON)
if(NOT EXISTS "${ARTIFACTS}/libjpeg-turbo-${JPEG_VERSION}/CMakeLists.txt")
  execute_process(COMMAND "${CMAKE_COMMAND}" -E tar xzf "libjpeg-turbo-${JPEG_VERSION}.tar.gz"
    WORKING_DIRECTORY "${ARTIFACTS}" RESULT_VARIABLE RESULT)
  if(NOT RESULT EQUAL 0)
    message(FATAL_ERROR "Could not extract libjpeg-turbo")
  endif()
endif()
