// stdafx.h : include file for standard system include files,
//  or project specific include files that are used frequently, but
//      are changed infrequently
//

#pragma once

//#include <ceconfig.h>
//#define _WIN32_WCE 5

// Change this value to use different versions
#define WINVER _WIN32_WCE
#define _SECURE_ATL 1

#define _WIN32_WCE_AYGSHELL 1
#define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA

#include <atlbase.h>
#include <atlwin.h>
#include <atltypes.h>

#include <assert.h>
#include <vector>
#include <string>
#include <deque>
#include <map>
#include <functional>
#include <sstream>
#include <set>
#include <iomanip>
#include <iosfwd>
#include <fstream>

#include <aygshell.h>
#pragma comment(lib, "aygshell.lib")

#pragma comment(lib, "Ole32.lib")

#ifdef lstrlenW
#undef lstrlenW
#endif
#define lstrlenW wcslen
