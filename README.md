# Aprendizaje automático con ml agents de Unity
Proyecto final de la asignatura de inteligencia artificial para videojuegos. Curso 2020/2021. Alumno: Felipe Cuadra Plaza.

* [Build del proyecto](https://drive.google.com/file/d/1B01IOrml7YZoQNTPSLsfFrg8Rn2u-toj/view?usp=sharing). Tambien está en el repositorio en la carpeta Ejecutable.

* [Vídeo del proyecto](https://drive.google.com/file/d/1rG9Mu1UtDIQilTecftvDPkR3bXPS0i6L/view?usp=sharing)
____________________________________________________________________________________________________________________________
## _Introducción_

> El plugin de aprendizaje automático o simplemente ML-Agents es un proyecto de código abierto de Unity, que permite que los juegos y simulaciones sirvan como entornos para entrenar a los agentes inteligentes. ML-Agents incluye una biblioteca de última generación para entrenar agentes para entornos 2D, 3D y VR.

>Los agentes pueden ser entrenados usando técnicas como aprendizaje de refuerzo, aprendizaje de imitación (estos dos son los que vamos a ver en este proyecto), neuro-evolución y otros métodos ML a través de una API de Python simple de usar. El plugin incluye una serie de opciones de capacitación, como Curriculum Learning para entornos de recompensa dispersa, auto-juego para escenarios de múltiples agentes y más.

>Estos agentes entrenados pueden ser utilizados para múltiples propósitos, incluyendo controlar el comportamiento de NPCs, pruebas automatizadas durante el desarrolo de juegos, así como evaluar varias decisiones de diseño del juego antes de su lanzamiento.

____________________________________________________________________________________________________________________________
## _Desarrollo del proyecto_

El proyecto consiste en realizar tres comportamientos más o menos simples usando los ML-Agents y empleando las técnicas de aprendizaje por refuerzo y aprendizaje por imitación. Primero de todo voy a explicar en que consiste cada método de aprendizaje.

El **_aprendizaje por refuerzo_** se basa en un bucle que consta de cuatro acciones como se puede ver en la imagen. 

![Captura1](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/AprendizajeRefuerzo.PNG)

La primera acción es la observación en la que el agente obtiene datos de su entorno de entrenamiento, la segunda es tomar una decisión en función de los datos que tenga, después realiza la acción que tenga que hacer y finalmente si realiza la accion correcta obtiene una recompensa, si no obtiene una penalización. Cos estas etapas, en un continuo bucle el agente, basandose en sus obsevaciones y acciones irá aprendiendo para conseguir las recompensas más altas. En resumen este método se basa en aprender mediante recompensas.

En cambio, el **_aprendizaje por imitación_** consiste en que el agente aprende de lo que un humano hace, es decir, en vez de probar acciones aleatorias (que sería como se aprende mediante aprendizaje refuerzo) va a tratar de imitar lo que he hecho el jugador. Este metodo es más útil cuando se tienen unos entornos más complejos y se requiere un comportamiento algo más difícil. Decir, que también se pueden aplicar recompensas y castigos al agente, para no solo imitar lo que hace el jugador si no también para mejorar lo que ha hecho.

____________________________________________________________________________________________________________________________
## _Guía rápida de instalacion del plugin ML-Agents en Unity_

Este proyecto se ha realizado en un PC con Windows 10, en linux o en mac desconozco el proceso de instalación, pero será parecido.

- Tener Unity instalado. En este proyecto se ha usado la versión 2019.4.21f.
- Tener instalado python. En este proyecto se ha usado la version 3.7.9.
- Abrir la consola de windows e ir a la carpeta raíz del proyecto de Unity.
- Crear un entorno virtual de python. Hacemos esto porque en caso de tener varios proyectos con diferentes versiones de ML-Agent, esto hace que cada uno sea independiente del otro, ya que cada uno tiene su propio entorno virtual. Para crear el entorno virtual ejecutamos el siguiente comando **python -m venv nombreCarpeta**, donde nombreCarpeta es el nombre de la carpeta donde se creará el entorno virtual. Tras esto deberíamos tener una carpeta con el nombre que hayamos puesto en el directorio raíz. Esta carpeta no es recomendable subirla a un repositorio porque pesa bastante.
- Activar el entorno virtual. Para ello nos metemos en la carpeta creada y luego en la carpeta Scripts y ejecutamos el **activate.bat** dese la consola.
- Actualizar el paquete de instalación de python llamado pip con este comando **python -m pip install --upgrade pip**
- Ejecutar **pip install torch==1.7.0 -f https://download.pytorch.org/whl/torch_stable.html** para instalar pytorch, que es una biblioteca de aprendizaje automático de código abierto. En este proyecto se ha usado la version 1.7.0.
- Instalar el paquete de los ml agents con este comando **python -m pip install mlagents==0.22.0**. En este proyecto se ha usado la version 0.22.0.
- Abrir el proyecto de Unity y desde el package manager, descargar el plugin de los ML-Agents. En este proyecto se ha usado la version 1.6.0.

Y listo, ya tendrás todo lo necesario.
____________________________________________________________________________________________________________________________
## _Desarrollo de los comportamientos_

Teniendo un proyecto de Unity vacío, cada comportamiento se va a desarrollar en una escena por separado. Decir también que una parte del tiempo dedicado al proyecto se ha empleado en investigar como funciona este plugin y aunque los comportamientos que vamos a hacer no son muy complejos, pueden servir para tener una buena base para poder hacer en un futuro comportamientos más complicados.

### _Primer comportamiento mediante aprendizaje por refuerzo (Escena1)_

Este comportamiento va a ser muy simple pero servirá para aprender lo básico de los ML-Agents de unity. Consiste en que un agente en este caso el cubo azul con ojos tiene que llegar a un objetivo, que es la esfera amarilla, en este pequeño entorno de entrenamiento rodeado por muros. Decir que la esfera y los muros son triggers y el agente tiene un rigidbody kinemático, ya que se va a mover mediante su transform.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Escena1.PNG)

Una vez hecho este escenario voy a añadir al agente una scripts que voy a llamar MoverHaciaObjetivo.cs y hay que hacer que la clase herede de Agent y no de MonoBehaviour, porque toda script que vaya a implementar comportamientos mediante ML-Agents debe heredar de Agent. Después vamos a agregar dos scripts propias del plugin de ML-Agents que son BehaviorParameters y DecisionRequester. También hay que saber que el algoritmo de machine learning de Unity solo entiende de números, es decir, que no sabe que es ir a la derecha, que es un vector, que es un transform, etc. Esto es algo que hay que asimilar bien, sobretodo a la hora de agregar observaciones y acciones a los agentes. Vamos a ver algunos de los atributos más importantes de la primera script:

![Captura3](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Refuerzo1.PNG)

- **Behavior Name**: es el nombre que va a tener el comportamiento.
- **Vector observations**: concretamente el Space Size es el número de observaciones que tiene que tener el agente.
- **Vector Action**: en el Space Type tiene dos tipos DISCRETE que representa números enteros y CONTINUOUS que representa números decimales. Si tipo es CONTINUOUS el Space Size representa el número de acciones del agente y si el tipo es DISCRETE, Branches Size también representa el número de acciones del agente, pero además hay que poner cuantos números (piensa que el número significa cuantas acciones va a tener esa rama, por ejemplo avanzar hacia delante, hacia atrás, etc) va a tener cada rama (acción) en los apartados de Branch 0 Size, Branch 1 size,etc. Por ejemplo, para entender esto mejor, piensa en la IA de un juego de coches. El coche tendría dos ramas/acciones, una tendría tamaño 2 que sería para acelerar y frenar y la otra rama tendría tamaño 3 para girar a la derecha, girar a la izquierda y para no girar.
- **Model**: aquí se agrega un archivo que es como el "cerebro" del agente para que desempeñe su comportamiento.
- **Behavior Type**: hay  tres tipos, DEFAULT que se usa cuando el agente está entrenando, HEURISTIC se usa cuando queremos controlar nosotros al agente para probar el entorno y INFERENCE que se usa cuando el agente ya tiene un cerebro.

La script llamada DecisionRequester, solicita una decisión cada cierto tiempo para llevar a cabo una acción. En esta script los parámetros se pueden dejar como están.

Al asignar la script creada que hereda de Agent en el inspector aparecerá una variable llamada **Max Step** que se refiere a la siguiente actualización en el entrenamiento que, por defecto se produce 15 veces por segundo como las físicas de unity, por ejemplo voy a asignarle un valor de 1000. Esto lo pongo para que el entrenamiento tenga un fin por si en algún momento el agente nunca es capaz de llegar al objetivo.
Tras saber esto vamos a empezar a escribir código en la script que se ha creado antes. Lo primero que hago es crear cuatro variables una que almacena la posición del objetivo, otra que guarda el MeshRenderer del suelo para cambiarle el material, ya que voy a hacer que cuando el agnete realize su tarea con éxito el suelo se ponga verde y si fracasa lo pongo rojo. Las otras dos variables son materiales con los colores.

Vamos a empezar por asignar al agente que observaciones tiene que tener en cuenta, para ello vamos a escribir un método de la clase Agente que se llama  CollectObservations(VectorSensor sensor). En este métodos añadiremos todo lo que el agente tiene que tener en cuenta con el método sensor.AddObservation(...). Aquí hay que pensar que es lo que necesita el agente para desempeñar su tarea, en este caso solo necesita saber su propia posición y la posición del objetivo. 

![Captura4](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Refuerzo2.PNG)

Tras haber puesto esto hay que volver a la script llamada BehaviorParameters y en el apartado de VectorObservation en el Space Size hay que poner 6. Pero ¿por qué 6 y no 2?, ya que solo se han añadido dos obsrvaciones. Como se ha dicho antes los ML-Agents solo entienden números y como las dos posiciones son un vector de 3 números cada uno, ponemos 6, porque son dos vectores cada uno de los cuales tiene tres números.

También vamos a cambiar en el apartado de Vector Action, en Space Type vamos a poner CONTINUOUS para que el agente tenga en cuenta números decimales y en Space Size vamos a poner 2 porque solo nos interesan dos acciones que son moverse en el eje X y moverse en el eje Z.

A continuación vamos a hacer que el agente recoja las acciones puestas antes, para ello vamos a utilizar el método OnActionReceived(float[] vectorAction). Aqui solo vamos a hacer que la primera posción del vector vectorAction sea para el movimiento en X y la segunda posición del vector que sea para el movimiento en el eje Z. Finalmente vamos a mover al agente mediante su transform.

![Captura5](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Refuerzo4.PNG)

Lo que nos falta es dar una recompensa o un castigo al agente. La recompensa se la vamos a dar cuando el agente choche con el objetivo y el castigo cuando choque con un muro, para ello vamos a recurrir a la funcion OnTriggerEnter de Unity. También vamos a recurrir a dos funciones de la clase Agent que son setReward(...) y EndEpisode(). La primera función sirve para establecer una recompensa o un castigo, dependiendo de si el número es positivo o negativo y la segunda sirve para finalizar el comportamiento. También cambio el color del suelo.

![Captura6](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Refuerzo6.PNG)

También ha que resetear el entorno cada vez que se empiece el comportamiento de nuevo, para ello recurrimos a una función que hereda de Agent llamada OnEpisodeBegin() que de momento solo va a hacer que el agente se coloque en el centro del escenario, el objetivo de momento va a estar siempre en la misma posición.

![Captura7](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Refuerzo3.PNG)

Por último hay que poder probar este entorno que hemos programado y creado por lo que vamos a añadir otra función que hereda de Agent llamada Heuristic(float[] actionsOut), que recoje el input del usuario que se va a enviar a la función OnActionReceived(float[] vectorAction). Las dos acciones las asignamos con el metodo getAxisRaw(...) de Unity para el movimiento en X y en Z.

![Captura8](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Refuerzo5.PNG)

Para poder probar el entorno hay que ir de nuevo a la script llamada BehaviorParameters y en el apartado de Behavior Type hay que poner HEURISTIC que ya deberías saber que signica esto. Ya solo queda darle al botón play de Unity y debería poder mover al agente con las teclas W,A,S,D y si choca con el objetivo o con un muro su posición se resetea al centro del entorno. Una vez que esto funcione correctamente podremos entrenar a nuestro agente.

Para entrenarlo hay que abrir la consola de windows en el directorio raíz del proyecto de Unity y activar el entorno virtual de pyhton. Una vez que se ha hecho esto hay que escribir el siguiente comando. 

![Captura9](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/ComportamientoBase.PNG)

- **mlagents-learn** es el comando que se encarga de entrenar a los agentes.
- **--run-id=nombre** es el nombre que le quieras dar a la carpeta que tendrá todos los archivos relacionados con el entrenamiento. Esta carpeta as u vez se guarda en una carpeta llamada results que está en el directorio raíz del proyecto de Unity.
- **--force** no es obligatorio ponerlo pero si quieres hacer varios entrenamientos con el mismo nombre tienes que poner esta opción para que sobreescriba los archivos que había antes, si no también puedes poner un nombre diferente a cada entrenamiento que hagas.

Una vez ejecutado este comando hay que cambiar el Behavior Type a DEFAULT y pulsar el botón del play de Unity y el agente se deberá mover por sí solo e intentará llegar al objetivo, como se puede ver en este [vídeo](https://drive.google.com/file/d/1nNEVJmDRaxbXNb82eXXHFGjbxX3CHiUZ/view?usp=sharing). Para salir del entrenamiento basta con parar la ejecución de Unity. Pero igual se tarda bastante en enseñar al agente si solo tenemos un entorno de entranmiento. Para que vaya más rápido voy a duplicar el entorno y voy a hacer 20 y voy a volver a ajecutar el comando de antes y como se puede ver en este [vídeo](https://drive.google.com/file/d/1e1uKbA2j8SJMKIkldEv5OjAeXa3H2s4r/view?usp=sharing) ahora hay 20 agentes entrenando a la vez. También decir que cuanto más tiempo dejes entrenando al agente más rápido y mejor hará su tarea. Como puedes ver al principo no muchos consiguen llegar al objetivo pero con el paso del tiempo el agente va aprendiendo y al final del video todos los agentes son capaces de llegar al objetivo. Después solo dejo activado un entorno de entrenamiento.

Una vez entrenado vamos a hacer que el agente use todo lo que ha aprendido durante su entrenamiento. Vamos a la carpeta results y buscamos una carpeta con el nombre que hayamos puesto en el comando ejecutado entes y nos metemos en esa carpeta y encontraremos un archivo con extensión .onnx que se llama como hayamos puesto en el parámetro de Behavior Name en la script BehaviorParameters. Este archivo es como el "cerebro" del agente que tiene toda la información del entrenamiento. Copiamos y pegamos ese archivo en la carpeta de assets y lo arrastramos al apartado Model del BehaviorParameters y el Behavior Type lo ponemos en INFERENCE. Ahora si ejecutamos el agente por si solo va a estar yendo hacia el objetivo, ya que para eso ha sido entrenado. Pero ¿qué pasa si cambiamos el objetivo de sitio? ¿lo hará igual de bien que antes? la respuesta es que no, como se puede ver en este [vídeo](https://drive.google.com/file/d/1kWBYx1Y4shWihoxdOEXBnkljbQh8lqeV/view?usp=sharing).¿Por qué ocurre esto? porque el agente ha sido entrenado para llegar al objetivo que está en una posición fija y si le cambiamos el entorno ya no sabe que hacer. Por ello vamos a mejorar un poco nuestra IA.

Para mejorar al agente vamos a hacer que el algoritmo que entrena use unos parámetros de configuración que pongamos nosotros, estos se configuran en un archivo con extensión .yaml. Primero voy a crear una carpeta en el directorio raíz de mi proyecto llamada config y ahí voy a crear con un editor de texto un archivo que lo voy a llamar MoverHaciaObjetivo.yaml (este archivo lo podeís ver en el repositorio de github de este proyecto). Una cosa importante antes de continuar, este archivo debe tener, al principio de este el mismo nombre que el apartado Behavior Name de la script BehaviorParameters.

Ahora en la consola de windows vamos a volver a ejecutar el comando de antes pero con una pequeña diferencia.

![Captura11](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Parametros.PNG)

Como segundo parámetro del comando le pasamos el archivo de configuración que está en la carpeta config. Tras esto ponemos el Behavior Type a DEFAULT y dejamos el archivo que es el "cerebro" en el apartado Model, vuelo a activar los entornos que habia desactivado antes y le damos al play. Como verás el entrenamiento es igual al anterior solo que hemos dicho que el agente entrene con unos parámetros de configuración dados.

Después lo que vamos a hacer es que el agente aparezca en una posición aleatoria en la parte izquierda del entorno y el objetivo va aparecer en una posición aleatoria en la parte derecha del entorno. También voy a hacer que el agente mire hacia el objetivo. Esto lo hacemos en la funcion OnEpisodeBegin().

![Captura10](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Refuerzo7.PNG)

Por último vamos a hacer que el agente entrene tomando como base el "cerebro" que sabe ir hacia el objetivo que tiene una posición fija. Para ello ejecutamos el siguiente parametro.

![Captura11](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/ComportamientoBaseMejorado.PNG)

El parámetro **--ititialize-from=nombreCarpeta** quiere decir que tomamos como base del entrenamiento el "cerebro" que hemos hecho con el primer entrenamiento. Ahí ponemos el nombre de la carpeta que tiene todos los archivos del "cerebro" que debe estar en la carpeta results y el nuevo "cerebro" que obtenga lo voy a guardar en una carpeta llamada ComportamientoBaseMejorado. Después ponemos el Behavior Type a DEFAULT y quitamos el archivo que es el "cerebro" en el apartado Model. También activamos todos los entornos de entrenamiento y presionamos el play. Como puedes ver en este [vídeo](https://drive.google.com/file/d/13goH2G_tYLZLkzbMdsdIKYUg24Aomyd1/view?usp=sharing) los agentes poco a poco sabrán llegar al objetivo esté donde esté. Todos los entrenamientos realizados hasta ahora han durado aproximadamente entre 2 y 3 minutos.

Tras el entrenamiento podemos desactivar todos los entrono menos uno, ponemos el Behavior Type a INFERENCE y añadimos el nuevo "cerebro" mejorado en el apartdo Model y así tendremos un maravilloso agente que es capaz de llegar un objetivo que le da igual donde esté. En este [vídeo](https://drive.google.com/file/d/1SvuQISbCOXpOJR2agVPRTHaGR6NK6xUj/view?usp=sharing) puedes ver el resultado final del primer comportamiento realizado, mediante aprendizaje por refuerzo.

Como última cosa podemos usar una herramienta llamada Tensorboard que sirve para visualizar mediante gráficas ciertos parámetros del comportamiento. Esta se puede activar con el siguiente comando desde la consola de windows. Después tienes que abrir un navegador web y poner localhost:6006 como se ve en la imagen. Esto lo puedes ejecutar durante (las gráficas van cambiando en tiempo real) o después de un entrenamiento.

![Captura12](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/tensorboard.PNG)

Esta herramienta permite ver bastantes parámetros pero en este caso vamos a ver dos que son bastante sencillos de entender.

![Captura13](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/GraficasCompBase.PNG)

- **Comulative Reward** muestra la media de recompensas que obtiene durante el entrenamiento. Si el agente entrena bien este valor debería subir con el paso del tiempo, ya que cada vez el agente va a fallar menos.
- **Episode Lenght** se refiere al tiempo que tarda el agente en conseguir su objetivo. Esto es normal que vaya bajando con el paso del tiempo, ya que el agente hará más rápido y mejor su tarea.

### _Segundo comportamiento mediante aprendizaje por imitación (Escena2)_

Este comportamiento va a ser un poco más complejo que el anterior por eso eso vamos a usar aprendizaje por imitación. La tarea consiste en que un agente, que es el cubo azul con ojos, tiene que llegar a un botón, que aparece en una zona aleatoria en la parte derecha del entorno, y pulsarlo. Esta acción hace aparecer un objetivo (una esfera amarilla) en la parte izquierda del entorno en una posición fija, al que tiene que ir el agente.
El entorno es exactamente igual que en el anterior comportamiento, solo que esta vez hay un botón, los muros no son triggers y el rigidbody del agente es cinemático, ya que lo vamos a mover mediante este.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/EntornoImitación.PNG)

Además de los componentes explicados en el anterior punto, hay que añadir uno nuevo al agente que se llama Demostration Recorder.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/demostrationRecorder.PNG)

- El checkbox **Record** sirve para indicar si queremos que nuestro agente imite las acciones que haga el usuario.
- **Demostration Name** es el nombre que vamos a darle al archivo, con extensión .demo, que va guardar los datos tras realizar la imitación de las acciones del usuario por parte del agente.
- **Demonstration Directory** es el nombre de la carpeta donde se van a guardar los archivos .demo. Esta carpeta se crea en el directorio raíz del proecto de unity.

Primero de todo vamos a completar la script Behavior Parameters. Hay que pensar cuantas observaciones necesita el agente para completar su tarea:
- Necesita saber si puede o no pulsar el botón (1 obsevación)
- También necesita saber si ha spawneado el objetivo (1 observación)
- En caso de que esté el objetivo, necesita saber el vector de dirección hacia este pero solo las coordenadas en X y en Z (2 observaciones)
- Por último, también necesita saber el vector de dirección hacia el botón pero solo las coordenadas en X y en Z (2 observaciones) 

En total son 6 observaciones.

El Space Type que va a tener es Discrete, ya que este caso nos interesan más los números enteros. Y va a tener 3 acciones dos de ellas (que representan el movimiento) van a tener 3 números y la otra (que representa pulsar o no el botón) va a tener solo 2.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/imitacion6.PNG)

Depués de configurar estos parámetros hayq que crear la script que hereda de Agent, que voy a llamar AgenteImitacion.cs y le voy a poner un Max Step de 5000000 y vamos a utilizar exactamente los mismos métodos que hemos usado en el anterior comportamiento. Vamos a ver que hay que poner en cada uno de ellos. Decir que la programación de interactuar con el botón y spawnear el objetivo, ya está hecha y no se va a explicar, ya que no tiene que ver con la IA, pero podéis mirarla en el repositorio para ver como está implementada.

Cuando empieza el comportamiento ponemos en una posición aleatoria al agente y reseteamos el botón, es decir, lo ponemos en una posición aleatoria,cambiamos su material y escala.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/imitacion1.PNG)

En cuanto a las observaciones añadimos las 6 mencionadas anteriormente.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/imitacion2.PNG)

Respecto a las acciones del agente usamos el mismo método que en el anterior comportamiento pero con otro parámetro, ya que está sobrecargado. Usamos este para poder obtener el vector de acciones discretas. Como se ha dicho antes dos acciones tienen tres números por que una hace referencia a no moverse,moverse hacia la izquierda y moverse hacia la derecha. La otra también hace referencia a referencia a no moverse, moverse hacia delante y moverse hacia atrás. La última solo tiene dos, ya que solo representa pulsar o no el botón. Decir también que le damos una recompensa al agente cuando le da al botón y también le añadmos una penalización en este método para hacer que el agente no tarde demasiado tiempo en llevar a cabo su tarea.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/imitacion3-1.PNG)
![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/imitacion3-2.PNG)

En la función Heuristic(in ActionBuffers actionsOut) recogemos las acciones del usuario de manera muy parecida al anterior comportamiento. Aquí también usamos este método, que también está sobrecargado, con otro parámetro para recoger el vector de acciones discretas.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/imitacion4.PNG)

Finalmente si el agente choca con el objetivo también le añadimos una recompensa, destruimos el objetivo y acabamos el comportamiento.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/imitacion5.PNG)

Como se puede ver, esto no es muy diferente al anterior comportamiento. Para verificar que todo funciona correctamente podéis probar el escenario poniendo el Behavior Type en HEURISTIC. Si os acercáis al botón y pulsáis la tecla E debe aparecer el objetivo y si chocáis con él acaba el comportamiento.

Una vez hecho esto vamos a hacer que el agente aprenda de nuestras acciones. Para ello hay que poner el Behavior Type en HEURISTIC y hay que marcar el checkbox Record de la script Demostration Recorder. Despúes de esto solo hay darle al botón play y realizar la tarea, es decir, ir al botón, pulsarlo e ir hacia el objetivo. Esto hay que hacerlo durante un rato y tras salir de la ejecución se habrá creado un archivo con extensión .demo en la carpeta Demos, donde se recoje toda la información que se ha obtenido mientras has jugado. Cuanto más tiempo dejes que la IA recoja nformación de lo que hagas, mejor será luego cuando la entrenemos.

A continuación en la carpeta config hay que crear un archivo que voy a llamar AgenteImitacion.yaml (podéis mirarlo en el repositorio) y en él hay que poner dos apartados para poder usar aprendizaje por imitación. Uno de ellos es **gail** (Generative Adversarial Imitation Learning) es el algoritmo de imitación qu se utiliza y hay ponerle dos atributos,aunque hay más. Uno es strength (va entre 0 y 1), que quiere decir cuanto va a impactar la demo al agente, es decir, si vale 1 va a intentar hacerlo muy similar a la demo hecha antes y si tiene un valor bajo va a intentar mejorar el comportamiento dado en la demo. Para empezar le pondré un valor de 0.5. El otro atributo que hay que poner es la ruta del archivo .demo realizado antes. Y hay que añadir otro apartado llamdo **behavioral_cloning** que es otro algoritmo de imitacion basado en hacer exactamente lo mismo que en la demo, por eso nunca va a mejorar el comportamiento dado en la demo y también tiene los mismos atributos. Combinando estos dos, se consigue un buen resultado.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/imitacionYaml.PNG) 

Para finalizar hay que poner el Behavior Type en DEFAULT, hay que desmarcar el checkbox Record de la script Demostration Recorder y voy a poner 20 entornos de entrenamiento. Y hay que ejecutar el comando, como en el anterior comportamiento, que permite entrenar con el archivo de configuración .yaml que se ha hecho antes y ya solo queda darle al play para que los agentes empiecen a antrenar como puedes ver en este [vídeo](https://drive.google.com/file/d/1hglTrqskki6MaiIYRn1_QJARl4rD_KiL/view?usp=sharing). El entrenamiento ha durado unos 3 minutos.

Después del entrenamiento solo queda añadir el archivo .onnx que es como el "cerebro" de nuestro agente ya tendremos el comportamiento terminado. Puedes verlo en este [vídeo](https://drive.google.com/file/d/12kNS6t5uUVIIlNWWxv7dARhJHM5GM7Df/view?usp=sharing). Seguramente toqueteando los atributos del archivo de configuración y entrenando a los agentes más tiempo conseguirás una IA lo haga más rápido.

Por último vamos a utilizar tensorboard para ver las gráficas.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/graficasImitación.PNG) 

En estas gráficas se pueden observar unas cuantas subidas y bajadas, pero por el final de ellas si que se llega a apreciar que el agente ha aprendido a hacer mejor su tarea, consiguiendo cada vez una recompensa mayor y tardando menos en realizarla.

### _Tercer comportamiento mediante aprendizaje por imitación (Escena3)_

En este comportamiento vamos ha hacer algo un poco distinto a los anteriores. Vamos a realizar una IA en una de las pala del Pong mediante apredizaje por imitación. Y la otra estará controlada por un jugador. Para ello he creado y programado un pequeño entorno de entrenamiento (en el repositorio podéis ver las scripts y todo lo relacionado con la realización de este escenario y la programación).

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/entornoEntrenamientoPONG.PNG) 

Antes de enpezar hay que poner dos gameobjects hijo a la pala y situar uno en un extremo de esta y el otro en el otro extremo de la pala. A estos dos gameobjects vamos a añadirle un componente de ML-Agents llamado **Ray Perception Sensor 2D**. Este componenete lanza raycasts para detectar cualquier elemento, que en nuestro caso son los muros de arriba y de abajo. Esto no esencial para desarrollar el comportamiento pero ayudará a que la pala no se que atascada en los muros.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/RayPerception.PNG) 

Poner un nombre diferente a cada componente para que no salga un error. Y en el resto de atributos lo que estamos haciendo es configurar diferentes parámetros para los raycast como el número de rayos que salen en una dirección, el máximo ángulo para cada rayo, la longitud de cada rayo, etc.

Lo primero que hay que hacer es añadir a nuestro agente todos los componentes necesarios (Behavior Parameters, Decision Requester y Demostration Recorder) y hay que crear una script que voy a llamar MLPala.cs a la que le voy a poner un Max Step de 4000. En esta script vamos a usar prácticamente los mismos métodos que en los anteriores comportamientos.

Antes de empezar a poner código vamos a pensar cuantas observaciones necesita el agente y que acciones va a realizar. Lo que el agente necesita saber de su entorno es:
- La posición de él mismo en el eje Y (1 observación)
- La posición de la pelota en el eje X e Y (2 observaciones)
- La velocidad de la pelota en el eje X e Y que obtenemos de su rigidbody (2 observaciones)

En total son 5 observaciones. Las añadimos en el método CollectObservations(VectorSensor sensor).

En cuanto a las acciones vamos a utilizar un espacio discreto y va a tener una acción de tamaño 3, porque la pala solo va a ir hacia arriba, hacia abajo o va a estar parada.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/behaviorParmPONG.PNG) 

Ahora si vamos a ver algunos de los métodos de la script creada antes (no vamos a ver todos en imágenes porque algunos son bastante fáciles de hacer).

Esta función la descubrí cuando estaba realizando este comportamiento y es muy similar a la función start o awake de unity. Me parecía curiosa ponerla, ya que no se había usado anteriormente.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/pong1.PNG) 

Como se ha dicho antes el agente solo tiene una acción que pueder ser estar parado, ir hacia arriba e ir hacia abajo. Asignamos la acción del buffer de acciones y limitamos la posición de la pala en el eje Y, aplicándole una penalización, para que no se pase de la posición del muro superior e inferior. Y si en algún momento está parada y se empieza a mover también le añadimos una penalización muy pequeña.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/pong2.PNG) 

Respecto a las acciones que recibe del usuario se la asignamos al buffer de acciones.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/pong3.PNG) 

Por último en la función OnTriggerEnter2D he hecho que si la pelota choca con la pala le doy una recompensa y si la pala choca con un muro le añado una penalización.
También tengo un método que añade una penalización que se llama cuando la pelota pasa por detrás de la pala, es decir, que esta no ha conseguido darle a la pelota.

Una vez hecha la script hay que seguir un proceso similar al anterior comportamiento por imitación. 
- Primero con el checkbox marcado del Demostration Recorder y el Behavior Type en HEURISTIC nos ponemos a controlar la pala y a darle a la pelota. Así se nos habrá creado un archivo .demo. 
- En segundo lugar, hay que crear un archivo de configuración que he llamado Pong.yaml (podéis verlo en el repositorio) y hay que agregarle los dos apartados mencionados antes, gail y behavioral_cloning. 
- En tercer lugar, para que el entrenamiento vaya más rápido podemos crear varios entornos y hay que entrenarlos con el archivo de configuración creado antes. En este [vídeo](https://drive.google.com/file/d/1NNFo29S0fjDt52kX5C9KFnsubrVGHVVw/view?usp=sharing) puedes ver como entrenan. En mi caso dejé a los agentes entrenando unos 40 minutos.

Ahora vamos a ver las gráficas con tensorboard.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/Captura.PNG) 

Aquí podemos ver la recompensa media obtenida ha ido en aumento a medida que avanzaba el entrenamiento, pero al principio el agente lo ha hecho muy mal consigiendo una recompensa negativa. La gráfica de Episode Lenght no interesa en este caso dado que no es importante el tiempo que tarde en conseguir la recompensa (en este esta caso en tensorboard en esta gráfica salía una linea paralela, indicando que el tiempo era el mismo).

Finalmente he añadido otra pala verde que controla el jugador con las teclas W y S, para jugar contra la pala roja controlada por la IA.

![Captura2](IAVFinal-Cuadra-Plaza/Assets/MaterialDocumentacion/entornoFinalPONG.PNG) 

Puedes ver como fnciona la IA con jugador en este [vídeo](https://drive.google.com/file/d/1pt7h-MzgKIVu717HUCusGtvat9ORAlBj/view?usp=sharing). Cabe descatar que la IA no es perfecta, a veces puede fallar pero casi siempre le da a la pelota. Seguramente toqueteando los atributos del archivo de configuración y entrenando a los agentes más tiempo conseguirás una IA que casi nunca falle. 

## _Final de proyecto y conclusiones_

Esto ha sido todo el proyecto. Espero que te haya gustado, te haya parecido interesante y que hayas aprendido lo básico sobre este increíble plugin de inteligencia artificial. En mi opinión me he gustado trabajar con esta herramienta y tiene muchas posibilidades para realizar casi cualquier comportamiento que se te ocurra. Lo que no me ha gustado es el hecho de tener que esperar mientras los agentes entrenan, que en este proyecto no eran tareas muy difíciles y no he estado mucho tiempo, pero si hay que hacer comportamientos más complejos pueden estar horas entrenando y eso no me agrada. Pero bueno, entiendo que hay que ser consciente de ello si te quieres meter de lleno en el aprendizaje automático.
