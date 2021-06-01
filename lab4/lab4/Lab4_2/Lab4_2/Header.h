#pragma once

#ifdef MATHLIBRARY_EXPORTS
#define MATHLIBRARY_API __declspec(dllexport)
#else
#define Calcs __declspec(dllimport)
#endif

extern "C" Calcs int Add(int a, int b);

extern "C" Calcs int Sub(int a, int b);

extern "C" Calcs float Average(float a, float b);

extern "C" Calcs int Mult(int a, int b);

extern "C" Calcs float Div(float a, float b);

extern "C" Calcs int Mod(int a, int b);

extern "C" Calcs int Factorial(int a);

extern "C" Calcs int Power(int a, int b);