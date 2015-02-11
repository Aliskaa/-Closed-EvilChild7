
public class IAreineNoire:IAreine
{
	public IAreineNoire (IAreaction maReaction):base(maReaction)
	{
		this.monType = TypesObjetsRencontres.REINE_NOIRE;
	}
	
	public override Oeuf genererOeuf(){
		float limiteOuvriere = modele.getRatioOuvriere ();
		System.Random random = new System.Random ();
		float resultat = (float)random.NextDouble();
		
		if (resultat <= limiteOuvriere) {
			resultat = (float)random.NextDouble();
			
			if(resultat < modele.getSpeciale()){
				return new Oeuf(TypesObjetsRencontres.CONTREMAITRE_NOIRE);
			}
			
			return new Oeuf(TypesObjetsRencontres.OUVRIERE_NOIRE);
		}
		
		resultat = (float)random.NextDouble();
		if (resultat < modele.getSpeciale ()) {
			return new Oeuf (TypesObjetsRencontres.GENERALE_NOIRE);
		}
		
		return new Oeuf(TypesObjetsRencontres.COMBATTANTE_NOIRE);
	}
}
