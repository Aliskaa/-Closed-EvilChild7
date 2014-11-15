using UnityEngine;
using System.Collections.Generic;

public interface IAappel:IAobjet{
	void signaler(List<Cible> objetsReperes);
}