#include "stdafx.h"
#include "stdlib.h"
#include "Trafikklys.h"


Trafikklys::Trafikklys(int left, int top, int right, int bottom, int hState) {
	leftPos = left;
	topPos = top;
	rightPos = right;
	bottomPos = bottom;
	state = hState;
}


Trafikklys::~Trafikklys() {

}

void Trafikklys::createTrafficLight(HDC hdc) {

}


