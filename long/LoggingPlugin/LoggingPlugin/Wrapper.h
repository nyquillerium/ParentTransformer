#pragma once
#include "LibSettings.h"

#ifdef __cplusplus
extern "C"
{
#endif
  LIB_API char* HelloWorld();
  LIB_API void Log(char *ObjectName, char *item, char *value);
  LIB_API char* LoadFile(char *FileName);
#ifdef __cplusplus
}
#endif