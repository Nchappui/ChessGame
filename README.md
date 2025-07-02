# ♟ ChessGame 3D
Un simulateur d’échecs en 3D développé sous Unity, intégrant les règles classiques des échecs, un mode contre IA basé sur Stockfish, ainsi qu’un mode multijoueur local.

## À propos du projet
Ce projet est une simulation complète du jeu d’échecs, réalisée avec le moteur Unity.
Il propose :

- Un plateau 3D réaliste, avec des pièces modélisées et des animations (victoire, promotion, déplacement).

- Un mode joueur contre joueur local, sur le même ordinateur.

- Un mode joueur contre IA, en utilisant le moteur d’échecs Stockfish intégré au projet.

- La possibilité de choisir la difficulté du bot de 1 à 20, pour s’adapter à tous les niveaux.

- Un timer optionnel (1, 3 ou 5 minutes) pour des parties plus compétitives.

## Fonctionnalités principales
- Règles officielles des échecs implémentées : mouvements légaux, échecs, échec et mat, promotions, etc.
- Choix du mode de jeu :

  - Jouer à deux sur le même PC.

  - Jouer contre l’IA avec choix du niveau (1-20).

- Choix du timer avant la partie (1min, 3min, 5min) ou désactivation pour jouer librement.
- Animations et effets visuels selon les événements du jeu (victoire, promotions, captures).
- Interface simple permettant de configurer rapidement la partie.

## Technologies et outils
- Développé avec Unity (version recommandée : 2021.3.x LTS ou supérieure)

- C# pour la logique du jeu

- Stockfish comme moteur d’IA, lancé en tant que processus externe. Le jeu dialogue avec Stockfish pour obtenir les coups et les appliquer petit à petit sur le plateau.

- Assets graphiques 3D importés (pièces)
