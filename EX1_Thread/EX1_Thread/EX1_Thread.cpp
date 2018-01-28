// EX1_Thread.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <windows.h>
#include "process.h"

int nThreads = 4;


int *pPrimes = NULL;
int nPrimes = 10000000;
int iPrimes = 4;
DWORD WINAPI ComputePrimes (LPVOID);
CRITICAL_SECTION critical_sec;
	
int main (int argc, char **argv)
{
	int i;

	if (argc == 2)
		nThreads = atoi(argv[1]);

	ULONGLONG t1 = GetTickCount64();

	//Allocate memory for the main thread's elements and initialize
	pPrimes = (int *) malloc(sizeof (int) * (nPrimes+1));
	for (i = 0; i<=nPrimes; i++)
		pPrimes[i] = 0;
	//Set the first prime number
	pPrimes[1] = 2;

	InitializeCriticalSection(&critical_sec);

	HANDLE* aH = (HANDLE*)malloc(nThreads*sizeof(HANDLE));
	int aiH[] = {1,2,3,4,5,6,7,8};

	//create threads
	for (int i=0; i < nThreads; i++)
		if((aH[i]=CreateThread (NULL, 0, ComputePrimes,
			(LPVOID)aiH[i],0,NULL)) == NULL)
			printf("Error: Failed to create Thread1\n");

	WaitForMultipleObjects(nThreads, aH, true, INFINITE);
	ULONGLONG t2 = GetTickCount64();

	// Print the computed Prime Numbers
	int cPrimes = 0;
	int cPrimesMax = 100;

	printf("Last %d primes before %d are: \n", cPrimesMax, nPrimes);
	printf("---------------------------------------- \n");
	for(i = nPrimes-1; i > 0; i--)
	{
		if(pPrimes[i] != 0) 
		{
			printf("%d ", pPrimes[i]);
			cPrimes++;
		}
		if (cPrimes == 100)
			break;
	}
	printf("\n");

	//close handles
	for (int i=0; i < nThreads; i++)
		CloseHandle (aH[i]);

	// free allocated memory
	if(pPrimes)
	{
		free(pPrimes);
		pPrimes = NULL;
	}

	printf("Number of threads: %d\n", nThreads);
	printf("Time			  : %d", t2-t1);
	getchar();
	return 0;
}


/***********************************************
* ComputePrimes(..): Thread Function used to compute primes
*************************************************/
DWORD WINAPI ComputePrimes(LPVOID idx)
{
	int currPrime = 0, i, sqroot;
	BOOL isPrime = TRUE;
	while (TRUE)
	{
		isPrime = TRUE;
		if (iPrimes <= nPrimes)
		{
			EnterCriticalSection (&critical_sec);
			{
				currPrime = iPrimes;
				iPrimes++;
			}
			LeaveCriticalSection (&critical_sec);
			sqroot = (int) sqrt ((double)currPrime)+1;
			for (i = 2; i<= sqroot; i++)
			{
				if((currPrime%i) == 0)
				{
					isPrime = FALSE;
					break;
				}
			}
		    EnterCriticalSection (&critical_sec);
			{
				if(isPrime)
					pPrimes[currPrime] = currPrime;
			}
		    LeaveCriticalSection (&critical_sec);
		}
		else
			return 0;
	}
	return 0;
}
