using UnityEngine;
using System.Collections.Generic;

public class BetisesTexturees
{
	private BetisesIndex maBetise;
	private Texture2D maTexture;

	public BetisesTexturees (BetisesIndex maBetise, Texture2D maTexture)
	{
		this.maBetise = maBetise;
		this.maTexture = maTexture;
	}

	public BetisesIndex getBetise(){
		return this.maBetise;
	}

	public Texture2D getTexture(){
		return this.maTexture;
	}

	public override string ToString(){
		switch (maBetise) {

		case BetisesIndex.CAILLOU:
			return "Caillou";	

		case BetisesIndex.PISTOLET:
			return "Pistolet a eau";

		case BetisesIndex.BONBON:
			return "Nourriture";

		case BetisesIndex.BOUT_DE_BOIS:
			return "Bout de bois";

		case BetisesIndex.DOUBLE_LANCER:
			return "Double Lancer";

		case BetisesIndex.LANCER_GROS:
			return "Lancer Gros";

		case BetisesIndex.VENTILATEUR:
			return "Ventilateur";

		case BetisesIndex.BOMBE_EAU:
			return "Bombe à eau";

		case BetisesIndex.SCARABEE:
			return "Scarabee";

		case BetisesIndex.GELEE:
			return "Gelee Royale";

		default:
			return "undefined";
    
		}
	}
}


