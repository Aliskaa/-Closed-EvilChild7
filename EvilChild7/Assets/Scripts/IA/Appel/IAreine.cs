using UnityEngine;
using System.Collections.Generic;

public class IAreine:IAabstraite
{
	
	protected IAreaction maReaction = null;
	protected Reine modele;
	protected TypesCamps monCamp;
	protected TypesObjetsRencontres monType;
	
	
	public IAreine(TypesObjetsRencontres monType, IAreaction reaction){
		attaquants = new List<IAabstraite> ();
		victimes = new List<IAabstraite> ();
		
		if (monType == TypesObjetsRencontres.REINE_BLANCHE) {
			monCamp = TypesCamps.BLANC;
			this.monType = TypesObjetsRencontres.REINE_BLANCHE;
		} else {
			monCamp = TypesCamps.NOIR;
			this.monType = TypesObjetsRencontres.REINE_NOIRE;
		}
		
		maReaction = reaction;
		
	}
	
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
	public Oeuf genererOeuf(){
		float limiteOuvriere = modele.getRatioOuvriere ();
		System.Random random = new System.Random ();
		float resultat = (float)random.NextDouble();
		
		if (resultat <= limiteOuvriere) {
			resultat = (float)random.NextDouble();
			if(resultat < modele.getSpeciale()){
				if(getCamp() == TypesCamps.BLANC){
					return new Oeuf(TypesObjetsRencontres.CONTREMAITRE_BLANCHE);
				}else{
					return new Oeuf(TypesObjetsRencontres.CONTREMAITRE_NOIRE);
				}
			}else{
				if(getCamp() == TypesCamps.BLANC){
					return new Oeuf(TypesObjetsRencontres.OUVRIERE_BLANCHE);
				}else{
					return new Oeuf(TypesObjetsRencontres.OUVRIERE_NOIRE);
				}
			}
		}else{
			resultat = (float)random.NextDouble();
			if(resultat <  modele.getSpeciale()){
				if(getCamp() == TypesCamps.BLANC){
					return new Oeuf(TypesObjetsRencontres.CONTREMAITRE_BLANCHE);
				}else{
					return new Oeuf(TypesObjetsRencontres.CONTREMAITRE_NOIRE);
				}
			}else{
				if(getCamp() == TypesCamps.BLANC){
					return new Oeuf(TypesObjetsRencontres.OUVRIERE_BLANCHE);
				}else{
					return new Oeuf(TypesObjetsRencontres.OUVRIERE_NOIRE);
				}
			}
			
		}
		
	}
	
	public void recevoirNourriture(){
		this.modele.augmenterQuantiteNourriture ();
	}
	
	public void recevoirNourriture(int quantite){
		this.modele.augmenterQuantiteNourriture (quantite);
	}
	
	
	public void ratiosLineaires(){
		modele.setRatioOuvriere (modele.getRatioOuvriere () - Reine.BAISSER_RATIO );
		modele.setSpeciale (modele.getSpeciale() + Reine.AUGMENTER_SPECIAL);
		
	}
}


