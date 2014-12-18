public class Dopage:Betise
{
	IAreine maReine;
	TypeDopage type;

	public Dopage(IAreine reine, TypeDopage type)
	{
		this.maReine = reine;
		this.type = type;

	}

	public void lancer(){
		this.maReine.doublerRatios(this.type);
	}

	public int getPoids(){
		return 0;
	}

	public void lancerCeTour(TourDeJeu tour){
		lancer ();
	}
}


