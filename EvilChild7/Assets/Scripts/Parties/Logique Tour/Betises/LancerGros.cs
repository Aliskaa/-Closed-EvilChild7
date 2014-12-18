
public class LancerGros:Betise
{

	private TourDeJeu monTour;

	public void lancer(){
		this.monTour.doublerPTAC ();
	}
	
	public int getPoids(){
		return 0;
	}

	public void lancerCeTour(TourDeJeu tour){
		this.monTour = tour;
		lancer ();
	}
}


