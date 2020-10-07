// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <cstdint>
#include <stdio.h>
#include <iostream>

typedef int32_t(_cdecl* hostfxr_main_fn)(const int argc, const wchar_t** argv);

void LoadCLR()
{
	AllocConsole();
	FILE* fDummy;
	freopen_s(&fDummy, "CONIN$", "r", stdin);
	freopen_s(&fDummy, "CONOUT$", "w", stderr);
	freopen_s(&fDummy, "CONOUT$", "w", stdout);

	printf("Loading hostfxr\n");
	HMODULE hostfxr = LoadLibrary(L"C:\\Program Files (x86)\\dotnet\\host\\fxr\\3.1.1\\hostfxr.dll");
	if (hostfxr != NULL)
	{
		printf("hostfxr.dll: 0x%p\n", hostfxr);
		hostfxr_main_fn main_fn;
		main_fn = (hostfxr_main_fn)GetProcAddress(hostfxr, "hostfxr_main");
		printf("main: 0x%p\n\n", main_fn);
		const wchar_t* args[2] = { L"run", L"C:\\Users\\user\\source\\repos\\Broken\\src\\Broken\\bin\\Release\\netcoreapp3.1\\win-x86\\publish\\Broken.dll" };
		main_fn(2, args);
	}
}

BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)LoadCLR, NULL, NULL, NULL);
		break;
	}
	return TRUE;
}

