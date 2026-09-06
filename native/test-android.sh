#!/usr/bin/env bash
set -euo pipefail
apk=$(find LibYuvSharp.Android.Test/bin/Release -name '*-Signed.apk' -print -quit)
test -n "$apk"
# Both ABIs must have made it into the consumer's APK.
unzip -l "$apk" | grep 'lib/arm64-v8a/libyuv_internal.so'
unzip -l "$apk" | grep 'lib/x86_64/libyuv_internal.so'
adb install -r "$apk"
adb logcat -c
adb shell am start -W -n dev.libyuvsharp.tests/dev.libyuvsharp.tests.MainActivity
for attempt in $(seq 1 30); do
  result=$(adb logcat -d -s LibYuvSharpTests:I '*:S')
  if [[ "$result" == *FAIL:* ]]; then echo "$result"; exit 1; fi
  if [[ "$result" == *PASS* ]]; then echo "$result"; exit 0; fi
  sleep 1
done
adb logcat -d
exit 1
