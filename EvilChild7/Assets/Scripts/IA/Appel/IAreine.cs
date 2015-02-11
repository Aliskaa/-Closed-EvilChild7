using UnityEngine;
using System.Collections.Generic;

public abstract class IAreine:IAabstraite
{
	
	protected IAreaction maReaction = null;
	protected Reine modele;
	protected TypesCamps monCamp;
	protected TypesObjetsRencontres monType;
	
	private bool flagDopageOuvriere = false;
	private bool flagDopageSoldat = false;
	
	
	public IAreine(IAreaction reaction){
		attaquants = new List<IAabstraite> ();
		victimes = new List<IAabstraite> ();
		modele = new Reine ();
		maReaction = reaction;
		
	}
	
	public  abstract Oeuf genererOeuf ();
	
	override public void signaler(List<Cible> objetsReperes){
		/* NOTHING TO SEE HEEEEEEEERE
		/*                                                   /===-_---~~~~~~~~~------____  */
		/*                                                 |===-~___                _,-'  */
		/*                   -==\\                         `//~\\   ~~~~`---.___.-~~  */
		/*              ______-==|                         | |  \\           _-~`  */
		/*        __--~~~  ,-/-==\\                        | |   `\        ,'  */
		/*      _-~       /'    |  \\                      / /      \      /  */
		/*    .'        /       |   \\                   /' /        \   /'  */
		/*   /  ____  /         |    \`\.__/-~~ ~ \ _ _/'  /          \/'  */
		/*  /-'~    ~~~~~---__  |     ~-/~         ( )   /'        _--~`  */
		/*                    \_|      /        _)   ;  ),   __--~~  */
		/*                      '~~--_/      _-~/-  / \   '-~ \  */
		/*                     {\__--_/}    / \\_>- )<__\      \  */
		/*                     /'   (_/  _-~  | |__>--<__|      |  */
		/*                    |0  0 _/) )-~     | |__>--<__|     |  */
		/*                    / /~ ,_/       / /__>---<__/      |  */
		/*                   o o _//        /-~_>---<__-~      /  */
		/*                   (^(~          /~_>---<__-      _-~  */
		/*                  ,/|           /__>--<__/     _-~  */
		/*               ,//('(          |__>--<__|     /                  .----_  */
		/*              ( ( '))          |__>--<__|    |                 /' _---_~\  */
		/*           `-)) )) (           |__>--<__|    |               /'  /     ~\`\  */
		/*          ,/,'//( (             \__>--<__\    \            /'  //        ||  */
		/*        ,( ( ((, ))              ~-__>--<_~-_  ~--____---~' _/'/        /'  */
		/*      `~/  )` ) ,/|                 ~-_~>--<_/-__       __-~ _/  */
		/*    ._-~//( )/ )) `                    ~~-'_/_/ /~~~~~~~__--~  */
		/*     ;'( ')/ ,)(                              ~~~~~~~~~~  */
		/*    ' ') '( (/ */
		/*      '   '  ` */
	}
	
	override public EntiteApointsDeVie getModele(){
		return this.modele;
	}
	
	override public TypesCamps getCamp(){
		return this.monCamp;
	}
	
	override public TypesObjetsRencontres retourType(){
		return this.monType;
	}
	
	override public void mort(){
		maReaction.mourir ();
	}
	
	public List<Oeuf> ponte(){
		List<Oeuf> mesOeufs = new List<Oeuf>();
		int quantite = modele.getQuantiteNourriture ();
		
		if(quantite > Reine.MAX_OEUFS){
			quantite = Reine.MAX_OEUFS;
		}
		
		for (int i = 0; i<quantite + 1; i++) {
			mesOeufs.Add (genererOeuf());
		}
		return mesOeufs;
	}
	
	public void recevoirNourriture(){
		this.modele.augmenterQuantiteNourriture ();
	}
	
	public void recevoirNourriture(int quantite){
		this.modele.augmenterQuantiteNourriture (quantite);
	}
	
	
	public void ratiosLineaires(){
		resetRatios ();
		modele.setRatioOuvriere (modele.getRatioOuvriere () - Reine.BAISSER_RATIO );
		modele.setSpeciale (modele.getSpeciale() + Reine.AUGMENTER_SPECIAL);
		
	}
	private void resetRatios (){
		if (flagDopageOuvriere) {
			flagDopageOuvriere = false;
			modele.setRatioOuvriere ( modele.getRatioOuvriere ()/2);
		}
		
		if (flagDopageSoldat) {
			flagDopageSoldat = false;
			modele.setRatioOuvriere ( modele.getRatioOuvriere ()*2);
		}
	}
	
	public void doublerRatios(TypeDopage type){
		resetRatios ();
		if (type == TypeDopage.OUVRIERE) {
			flagDopageOuvriere = true;
			modele.setRatioOuvriere (modele.getRatioOuvriere () * 2);
		} else {
			flagDopageSoldat = true;
			modele.setRatioOuvriere (modele.getRatioOuvriere () / 2);
		}
		
		
	}
}


