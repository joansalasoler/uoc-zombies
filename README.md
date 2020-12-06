PEC 2 - First Person Shooter
============================

¿Qué es?
--------

«Boondocks Of The Living Dead» es un videojuego inspirado en las películas de
serie B de George A. Romero. Una história de zombies en gama de grises en la
que los muertos se mueven muy lentamente.

Patricia, —estudiante de biología de día, canguro de noche, y gran aficionada
al psychobilly— se encuentra en algún lugar remoto de islas británicas con la
única misión de encontrar los libros de hechizos de La Voisin, con los que
podrá devolver, de una vez por todas, los come-cerebros a sus tumbas.

Vídeo de demostración
---------------------

![Demo](Resources/demo.webm)

¿Qué se ha implementado?
------------------------

* Un pequeño pueblo y su cementerio ubicados en un terrain de Unity.
* El personaje principal dispone de una arma con la que disparar.
* Animación de las acciones del protagonista y los zombies.
* Cantidad de vida, munición y escudos de protección visibles en el HUD.
* Los zombies persiguen al jugador y lo atacan cuando está cerca.
* Explosiones de sangre cuando se hiere al jugador o a los zombies.
* Cajas de vida, munición y escudos repartidos por el escenario.
* Pantalla de fin de partida y posibilidad de reinicio.
* Situaciones dónde es necesario saltar para subir a los tejados.
* Efectos de sonido, ambientación y música.
* Tres tipos diferentes de zombies con diversas propiedades.
* Los dragones dejan ítems de vida en el suelo al matarlos.

Detalles de la implementación
-----------------------------

Para la implementación del juego se ha partido del escenario y el código
desarrollada para la anterior PAC. Aunque se ha mejorado substancialmente el
sistema de inteligencia artificial y de adaptado el terreno para que sea
usable por el personaje en tercera persona.

Las partes nuevas del código que son más relevantes son las siguientes:

* Shared/AI/ - Inteligencia artificial de los zombies y los dragones. En el
  caso de los dragones se ha reusado la IA existente aunque sería necesario
  refactorizarla para usar el nuevo sistema.

  * Shared/AI/Actor/ - Estados de los zombies: patrullar (PatrolState),
    perseguir al jugador (ChaseState) y morir (DieState).

  * Shared/AI/ZombieController - Controlador de la IA de los zombies.

  * Shared/AI/ZombieLocomotion - Sincroniza las animaciones de los zombies
    con el NavMesh Agent. Se basa en dos blend trees, uno contiene las
    animaciones de caminar en diferentes ángulos y velocidades, y el otro
    las de girarse en distintos ángulos.

    Aunque la sincronización hace que la animación sea bastante creíble,
    debería pulirse el código que se encarga de los giros pues provoca que
    en algunos casos se pierda la sincronización y suceda que los zombies
    lleguen incluso a atravesar paredes.

  * Shared/AI/ZombieRotation - Se encarga de girar los zombies sobre su
    eje vertical para que sigan la normal del suelo y así evitar que los
    pies de los actores se hundan o floten sobre el suelo.

* Scripts/Shared/Controllers/PlayerController - Se ha adaptado y simplificado
  el controlador del jugador de la anterior PAC para su uso en la actual.

* Scripts/Shared/Controllers/WeaponController - Controlador de las propiedades
  y disparos del arma del jugador. Se ha adaptado del código de la anterior
  PAC aunque con algunas mejoras.

* Scripts/Shared/Effects - Efectos de luz, decals y partículas. Controlan el
  paso del día inclinando la luz solar, el encendido y apagado de las farolas
  y la luz de los relámpagos entre otros.

* Scripts/Shared/Handlers - Acciones que se ejecutan en las colisiones.

Respecto a las animaciones, el controlador de animación de los zombies se ha
creado des de cero partiendo de animaciones encontradas en Mixamo. En este caso,
las animaciones de daño, ataque y muerte se aplican en la parte superior del
cuerpo mediante una capa de animación separada. El daño usa un blend tree para
distinguir la dirección del impacto y animar al zombie consecuentemente.

En el caso de las animaciones de la protagonista, se ha partido del controlador
de animación del ThirdPersonController al que se le ha añadido una capa de daño
de forma análoga a la animación de los zombies. También se le han añadido
animaciones de muerte y disparo.

En este último caso, de animación de disparos, esta no se sobrepone a las
animaciones de correr/daño pues pareció más adecuado y divertido, dada la
velocidad a la que se mueven los zombies, que el personaje tenga que pararse
a apuntar y recargar después de cada disparo.

La última versión
-----------------

Puede encontrar información sobre la última versión de este software y su
desarrollo actual en https://gitlab.com/joansala/uoc-zombies

Referencias
-----------

Todos los modelos 3D y sonidos del juego se han publicado con licéncias que
permiten la reutilización y distribución. Son propiedad intelectual de sus
respectivos authores. Algunos de los recursos se han creado o modificado
exclusivamente para su uso en el juego por el autor, Joan Sala Soler.


Thunderstorm
https://freesound.org/people/ophylia/sounds/474994/

It is Coming (music)
https://www.fesliyanstudios.com/royalty-free-music/download/it-is-coming/262

Feast of the Flesh
https://www.dafont.com/feast-of-flesh-bb.font

Crickets Ambiance
https://freesound.org/people/Sound_2425/sounds/410282/

Owl
https://freesound.org/people/freemaster2/sounds/187669/

Wood Creak
https://freesound.org/people/cclaretc/sounds/346140/

GunShot
https://freesound.org/people/okieactor/sounds/415912/

Bullet Hole
https://picsart.com/i/sticker-art-hole-268803725027211

Books
https://assetstore.unity.com/packages/3d/props/interior/books-3356

quick female scream
https://freesound.org/people/AmeAngelofSin/sounds/168725/

pain: ouch
https://freesound.org/people/AmeAngelofSin/sounds/234039/

Short scared scream
https://freesound.org/people/jorickhoofd/sounds/180344/

Blood Hitting Window
https://freesound.org/people/Rock%20Savage/sounds/81042/

Zombie Voice
https://freesound.org/people/PatrickLieberkind/sounds/213835/
