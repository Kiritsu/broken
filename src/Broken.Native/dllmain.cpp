// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <cstdint>
#include <iostream>
#include <stdlib.h>

typedef int32_t(_cdecl* hostfxr_main_fn)(const int argc, const wchar_t** argv);

void InitConsole()
{
	AllocConsole();
	FILE* fDummy;
	freopen_s(&fDummy, "CONIN$", "r", stdin);
	freopen_s(&fDummy, "CONOUT$", "w", stderr);
	freopen_s(&fDummy, "CONOUT$", "w", stdout);
}

const wchar_t* WGetEnvSecure(const wchar_t* name)
{
	size_t requiredSize;
	_wgetenv_s(&requiredSize, NULL, 0, name);
	if (requiredSize == 0)
	{
		return NULL;
	}

	wchar_t* content = (wchar_t*)malloc(requiredSize * sizeof(wchar_t));
	if (!content)
	{
		return NULL;
	}

	_wgetenv_s(&requiredSize, content, 0, name);
	return content;
}

void LoadCLR()
{
	InitConsole();

	size_t requiredSize;
	const wchar_t* hostFxrPath = WGetEnvSecure(L"BROKEN_HOSTFXR_PATH");
	if (hostFxrPath == NULL)
	{
		hostFxrPath = L"C:\\Program Files (x86)\\dotnet\\host\\fxr\\3.1.1\\hostfxr.dll";
	}

	const wchar_t* dllPath = WGetEnvSecure(L"BROKEN_NET_ASSEMBLY_PATH");
	if (dllPath == NULL)
	{
		dllPath = L"Y:\\broken\\Broken.dll";
	}

	printf("Loading hostfxr.dll\n");
	HMODULE hostfxr = LoadLibrary(hostFxrPath);
	if (hostfxr == NULL)
	{
		return;
	}

	printf("Address of hostfxr.dll: 0x%p\n", hostfxr);
	hostfxr_main_fn main_fn = (hostfxr_main_fn)GetProcAddress(hostfxr, "hostfxr_main");
	if (main_fn == NULL)
	{
		return;
	}

	printf("Address of hostfxr_main: 0x%p\n\n", main_fn);
	const wchar_t* args[2] = { L"run", dllPath };
	main_fn(2, args);
}

BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	if (ul_reason_for_call != DLL_PROCESS_ATTACH)
	{
		return TRUE;
	}

	CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)LoadCLR, NULL, NULL, NULL);
	return TRUE;
}

