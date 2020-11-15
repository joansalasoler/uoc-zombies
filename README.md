PEC 2 - First Person Shooter
============================

¿Qué es?
--------

Sin saber muy bien cómo o por qué, nos despertamos en una isla desconocida muy
lejos de nuestra casa. Con el objetivo de despegar nuestra nave espacial para
poder salir de ahí y armados únicamente con nuestras pistolas de agua, deberemos
adentrarnos en el bosque camino del castillo.

Usaremos nuestro controlador para caminar, correr, saltar, disparar, cambiar
de arma y mirar a nuestro alrededor. El trayecto no será fácil, pues tanto la isla como el castillo se encuentran repletos de dragones escupe-agua.

* Los dragones de fuego son peligrosos; huye si te los encuentras.

* Luego están los dragones verdes que son más molestos que peligrosos.

* Los dragones azules de agua, que son inofensivos y intentaran no cruzarse en
  nuestro camino, están repletos de munición para nuestras armas.

* Los dragones de metal nos ofrecerán escudos para protegernos, aunque deberemos
  acercarnos a ellos con cautela.

* El mega-dragón es el más difícil de vencer. Seguro nos pondrá en apuros pero
  también nos dará buenas recompensas. Hay que tener especial cuidado si va
  acompañado de algunos de sus esbirros.

Nuestro deposito de agua es pequeño, así que si nos quedamos sin munición podemos
obtenerla de alguna de las caja de agua que encontraremos, de uno de los dragones
azules si conseguimos alcanzarlo o nos podemos zambullir en el océano si tenemos
tiempo para ello.

Vídeo de demostración
---------------------

![Demo](Resources/demo.webm)

¿Qué se ha implementado?
------------------------

* Escenario compuesto por un _terrain_ y un castillo.
* Dos tipos de armas de cadencia, precisión y alcance distintos.
* Lógica de daño del personaje y los enemigos.
* Visualización de la vida  escudos y munición del protagonista.
* Desplazamiento del protagonista por plataformas móviles.
* Lógica de llaves y puertas cerradas en el castillo.
* Inteligencia artificial de los enemigos.
* Cajas de recompensa y recompensas al dañar a los enemigos.
* Pantalla final y posibilidad de reiniciar la partida.
* Efectos de sonido y música.
* Diferentes tipos de enemigos con comportamientos diferenciados.

Detalles de la implementación
-----------------------------

En cuanto a la implementación, los controladores más relevantes són los
siguientes que pueden encontrarse en el paquete Shared/Controllers.

* PlayerController - Lógica de estado del personaje. El estado el jugador se
  guarda en el _scriptable object_ Models/Actors/PlayerStatus, y contiene la
  lógica de munición, vida, escudos y llaves encontradas.

* WeaponController - Lógica de las armas y animación de las mismas. Se decide
  aquí cómo son los disparos según el tipo de arma, dónde se incrustan los
  _decal_ de los proyectiles y qué sucede a los objetos contra los que se
  dispara (si se mueven o sufren daño en el caso de los dragones).

* MonsterController - Lógica de animación de los enemigos y de su estado. La
  inteligencia de los dragones se implementa en una máquina de estados que
  puede encontrarse en la carpeta Shared/States/Monsters

* OceanController - Animación del océano que rodea la isla. La lógica por la
  cual se recarga la munición del jugador al entrar en el oceano se implementa
  en la calse Handlers/Rewards/OnOceanTrigger

La inteligencia artificial de los enemigos se implementa usando _navmesh agents_
de manera que cuando un dragon está patrullando se decide que camino seguir y
en qué dirección. La implementación de los caminos a seguir se encuentra en las
clases /Shared/Models/Paths/Waypath y /Shared/Models/Paths/Waypoint.

* Waypath - Describe un camino como un conjunto de puntos (Watpoint). El camino
  puede ser lineal, circular o aleatorio. Si el camino es circular el dragón
  seguirá la ruta por el primer punto del camino cuando este termine, en caso
  de no serlo, canviará de dirección y recorrerá el camino por el que ha venido.

* Waypoint - Es un punto en uno o varios caminos (Waypath). Si pertenece a
  varios camino será una intersección y el dragón decidirá al llegar a el si
  sigue por el mismo camino o coje un camino distinto.

En cuanto a los estados de la inteligencia artificial:

* AlertState - El jugador está cerca del dragón. El dragón se gira en dirección
  al jugador y si lo encuentra pasará a atacarlo.

* AttackState - Mientras se encuentre en este estado el dragón lanzará bolas de
  agua para dañar al jugador.

* DieState - Estado final de la máquina. El dragón ha sido dañado de muerte.

* PainState - El jugador ha dañado al dragón y este se pondrá en estado de alerta.

* PanicState - El dragón huye espantado del jugador. Esto ocurrirá si el dragón
  es de agua o estava patrullando y sólo le queda un punto de vida.

* PatrolState - El dragón está recorriendo uno o varios caminos.

* WaitState - El dragón se encuentra quieto.

Así mismo, en el paquete Shared/Handlers se implementa la lógica de eventos y
colisiones del personaje con su entorno. Los más importantes son los siguientes.

* OnPlayerCollision - Cuando el personaje choca con un objeto etiquetado como
  movible, lo empuja con una fuerza proporcional a su velocidad. Por otra parte,
  si el actor sube en una plataforma móvil hace que este se desplace junto a
  ella añadiendo a la posición del jugador la velocidad de la plataforma.

* OnDoorTrigger - Abre las puertas si el jugador tiene la llave correcta.

* OnElevatorTrigger - Activa el ascensor cuando el jugador se sube encima.

* Handlers/Shared/Rewards - Todos estos _handlers_ añaden recursos al estado
  del jugador (PlayerStatus) cuando este colisiona con las cajas de recompensa.

Para los sonidos se ha creado el servicio Services/AudioService que contiene
un singleton encargado de reproducirlos en alguno de los múltiples AudioSource
que contienen los objetos del juego.

Para poder centralizar los sonidos, estos se configuran en la clase AudioTheme y
se les asigna un nombre de evento. Luego las clases de Shared/Handlers los
reproducen pasando un nombre de evento y AudioSource al servicio AudioService.

La última versión
-----------------

Puede encontrar información sobre la última versión de este software y su
desarrollo actual en https://gitlab.com/joansala/uoc-shooter

Referencias
-----------

Todos los modelos 3D y sonidos del juego se han publicado con licéncias que
permiten la reutilización y distribución. Son propiedad intelectual de sus
respectivos authores. Algunos de los recursos se han creado o modificado
exclusivamente para su uso en el juego por el autor, Joan Sala Soler.
