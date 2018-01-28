// EX0_Console.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "stdlib.h"

int Sum(int *p, int size)
{
	int sum = 0;
	for (int i = 0; i < size; i++)		  
	{ 
		   sum = sum + *p; 
		   p = p + 1; // compact way to write: sum += *p++; 
	}
	return sum;
}

void SwapPtr(int** pp1, int **pp2)
{
	int  *tmp;
	tmp  = *pp1;
	*pp1 = *pp2;
	*pp2 = tmp ;
}


int main()
{
	// An array name is a constant pointer to the first element of the array. 
	int aCube[] = {0, 1,8,27,64,125};
	int *pCube = aCube;
	int n3 = sizeof(aCube) / sizeof(int);  // DO NOT USE sizeof(pCube) -> its 4

	printf("aCube values : \n");
	for (int i = 0; i < n3; i++) 
		printf("%i %d %p\n",i, aCube[i], &aCube[i]);


	// 1 The same adress
	printf("\naCube    : %p\n",  aCube);  // OK
	printf( "&aCube    : %p\n" , &aCube); // Frustrating

	// Not the same adress
	printf("\npCube    : %p\n", pCube);  // OK
	printf(  "&pCube   : %p\n", &pCube); // OK


	// pCube++ Legal - aCube++; is illegal	
	pCube+=3;  // 
	printf("Inc 3 - pCube: %p\n", pCube);   // adress of pCube
	printf("Element 5 : %d\n", *(pCube + 2)); // 2+3 = 5
	pCube-=3; // points to aCube[0] again

	// Differences in size
	printf("Size aCube %d\n Size pCube %d", sizeof(aCube), sizeof(pCube));

	getchar();


	// 3 Call function
	printf("Sum aCube  : %d \n", Sum(aCube, n3));
	printf("Sum partial: %d \n", Sum(aCube+3, n3-3));

	// 4 Dynamic memory allocation
	int *pMem;
	pMem = (int*)malloc(sizeof(int) * n3);
	memcpy(pMem, aCube, sizeof(int) * n3);
	
	// SWAP test Pointer to pointer
	int *pa = pMem+1 ,  *pb = pMem+4;
	printf("\nSWAP test\n");
	printf("   *pa %d pb %d\n", *pa, *pb); 
	SwapPtr(&pa, &pb);
	printf("   *pa %d pb %d\n", *pa, *pb);
	
	free(pMem);
	getchar();

    return 0;
}

// printf("Sizeof aCube=%d  a2=%d p=%d", sizeof(aCube), sizeof(a2), sizeof(p));
//// Same results
//printf("Elememt 3:  %d\n", aCube[3]);
//printf("Elememt 3:  %d\n", *(aCube+3));
//printf("Elememt 3:  %d\n", pCube[3]);
//printf("Elememt 3:  %d\n", *(pCube + 3));
//printf("&pCube[0]: %p\n", &pCube[0]);  // OK 
//printf("  pCube[0] : %p\n", pCube[0]);  // OK 