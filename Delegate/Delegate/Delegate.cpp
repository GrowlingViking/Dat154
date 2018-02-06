// Delegate.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

typedef void(*PF)(int *);

class Delegate {
public:
	int n;
	PF rg[100];

	Delegate()
	{
		n = 0;
	}
	void operator +=(PF pf)
	{
		rg[n++] = pf;
	}
	void Invoke(int *pv)
	{
		for (int i = 0; i < n; i++)
			rg[i](pv);
	}
};

void Double(int* p) {	*p *= 2; }
void Tripple(int* p) { *p *= 3; }
void Square(int* p) { *p  = *p * *p; }

void Quad(int* p)
{
	*p *= 4;
}

int main()
{
	Delegate d;
	d += Double;
	d += Tripple;
	d += Square;
	d += Quad;

	int a = 4;

	d.Invoke(&a);

	printf("(C++) Value is : %d", a);
	getchar();
}








//typedef  void (*PF)(int*);
//
//class Delegate {
//public:
//	Delegate::Delegate()
//	{
//		i = 0;
//	}
//
//	void operator +=(PF p) { a[i++] = p; }
//	void Invoke(int *pi) { 
//		for (int x = 0; x < i; x++) 
//			a[x](pi); 
//	}
//private:
//	PF a[100]; 
//	int i;
//	};
//
//void Double(int* p) { *p *= 2; }
//void Tripple(int* p) { *p *= 3; }
//void Square(int *p) { *p = *p * *p; }
//
//int main()
//{
//
//	Delegate d;
//
//
//	d += Double;
//	d += Tripple;
//	d += Square;
//
//	int a = 4;
//	d.Invoke(&a);
//
//	printf("(C++) Value is : %d", a);
//	getchar();
//
//    return 0;
//}

