using UnityEngine;
using System.Collections.Generic;

public interface IAreaction{
	
	void bouger(TypesAxes direction, int nbCases);
	TypesAxes deambuler();
	void mourir();
	TypesAxes rentrerBase();
	void poserPheromones(bool poser);
	
}


