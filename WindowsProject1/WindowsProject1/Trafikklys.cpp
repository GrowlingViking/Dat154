#include "stdafx.h"
#include "stdlib.h"
#include "Trafikklys.h"


Trafikklys::Trafikklys(HDC hdc, int x, int y, int hState) {
	hDC = hdc;
	X = x;
	Y = y;
	state = hState;

	createTrafficLights();
}


Trafikklys::~Trafikklys() {

}

void Trafikklys::createTrafficLights() {
	
	// Creating the brushes
	HBRUSH blackBrush = CreateSolidBrush(RGB(0, 0, 0));
	HBRUSH greyBrush = CreateSolidBrush(RGB(124, 124, 124));
	HBRUSH redBrush = CreateSolidBrush(RGB(200, 0, 0));
	HBRUSH yellowBrush = CreateSolidBrush(RGB(250, 225, 55));
	HBRUSH greenBrush = CreateSolidBrush(RGB(90, 235, 10));
	
	HGDIOBJ hOrg = SelectObject(hDC, blackBrush);

	// The X and Y is the center of the trafficlight
	Rectangle(hDC, X - 25, Y - 75, X + 25, Y + 75);

	switch (state) {
	
	case 0: 
		
		SelectObject(hDC, redBrush);
		Ellipse(hDC, X - 20, Y - 70, X + 20, Y - 30);

		SelectObject(hDC, greyBrush);
		Ellipse(hDC, X - 20, Y - 20, X + 20, Y + 20);
		Ellipse(hDC, X - 20, Y + 30, X + 20, Y + 70);

		break;
	case 1:

		SelectObject(hDC, redBrush);
		Ellipse(hDC, X - 20, Y - 70, X + 20, Y - 30);

		SelectObject(hDC, yellowBrush);
		Ellipse(hDC, X - 20, Y - 20, X + 20, Y + 20);

		SelectObject(hDC, greyBrush);
		Ellipse(hDC, X - 20, Y + 30, X + 20, Y + 70);

		break;
	case 2:

		SelectObject(hDC, greyBrush);
		Ellipse(hDC, X - 20, Y - 70, X + 20, Y - 30);
		Ellipse(hDC, X - 20, Y - 20, X + 20, Y + 20);

		SelectObject(hDC, greenBrush);
		Ellipse(hDC, X - 20, Y + 30, X + 20, Y + 70);
		break;
	case 3:


		SelectObject(hDC, greyBrush);
		Ellipse(hDC, X - 20, Y - 70, X + 20, Y - 30);

		SelectObject(hDC, yellowBrush);
		Ellipse(hDC, X - 20, Y - 20, X + 20, Y + 20);

		SelectObject(hDC, greyBrush);
		Ellipse(hDC, X - 20, Y + 30, X + 20, Y + 70);

		break;
	}

	// Cleanup
	SelectObject(hDC, hOrg);
	DeleteObject(blackBrush);
	DeleteObject(greyBrush);
	DeleteObject(redBrush);
	DeleteObject(yellowBrush);
	DeleteObject(greenBrush);
}




