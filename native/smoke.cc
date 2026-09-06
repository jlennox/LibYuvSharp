#include <stdio.h>
#include <stdint.h>
#include <vector>
#include "libyuv.h"

int main(int argc, char** argv) {
  if (argc != 2) return 1;
  FILE* file = fopen(argv[1], "rb");
  if (!file) return 2;
  std::vector<uint8_t> jpeg;
  int value;
  while ((value = fgetc(file)) != EOF) jpeg.push_back(static_cast<uint8_t>(value));
  fclose(file);
  int width = 0, height = 0;
  if (libyuv::MJPGSize(jpeg.data(), jpeg.size(), &width, &height) || width != 16 || height != 16) return 3;
  uint8_t y[256], u[64], v[64], argb[1024];
  if (libyuv::MJPGToI420(jpeg.data(), jpeg.size(), y, 16, u, 8, v, 8, 16, 16, 16, 16)) return 4;
  if (libyuv::J420ToARGB(y, 16, u, 8, v, 8, argb, 64, 16, 16)) return 5;
  for (int i = 0; i < 1024; i += 4) {
    if (argb[i] < 46 || argb[i] > 54 || argb[i + 1] < 96 || argb[i + 1] > 104 ||
        argb[i + 2] < 196 || argb[i + 2] > 204 || argb[i + 3] != 255) return 6;
  }
  uint8_t rgb[768], roundtrip[1024];
  if (libyuv::ARGBToRGB24(argb, 64, rgb, 48, 16, 16) ||
      libyuv::RGB24ToARGB(rgb, 48, roundtrip, 64, 16, 16)) return 7;
  for (int i = 0; i < 1024; ++i) if (argb[i] != roundtrip[i]) return 8;
  if (!libyuv::MJPGSize(jpeg.data(), 32, &width, &height)) return 9;
  puts("JPEG decode, color conversion, round trip, and truncated input passed.");
  return 0;
}
