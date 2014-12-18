public enum PoidsObjetsAlancer : 
int { 
	POIDS_CAILLOU = 2,
	POIDS_BOMBE_A_EAU = 1,
	POIDS_SCARABEE = 1,
	POIDS_OEUFS = 1,
	POIDS_BONBON = 1,
	POIDS_BOUT_DE_BOIS = 1,
	POIDS_RIEN = 0
}
public static class PoidsObjetsAlancerM{
	public static PoidsObjetsAlancer indexToPoids(BetisesIndex index){
		switch(index){
			case BetisesIndex.CAILLOU:
				return PoidsObjetsAlancer.POIDS_CAILLOU;
		
			case BetisesIndex.BONBON:
				return PoidsObjetsAlancer.POIDS_BONBON;
				
			case BetisesIndex.BOUT_DE_BOIS:
				return PoidsObjetsAlancer.POIDS_BOUT_DE_BOIS;
				
			case BetisesIndex.BOMBE_EAU:
				return PoidsObjetsAlancer.POIDS_BOMBE_A_EAU;
				
			case BetisesIndex.SCARABEE:
				return PoidsObjetsAlancer.POIDS_SCARABEE;

			default:
				return PoidsObjetsAlancer.POIDS_RIEN;
		}

	}
}
