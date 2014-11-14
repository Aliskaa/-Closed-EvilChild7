using UnityEngine;
using System.Collections.Generic;

public interface IAreaction{
	
	void bouger(TypesAxes direction, int nbCases);
	void deambuler();
	void mourir();
	void rentrerBase();
	void poserPheromones();
	
}