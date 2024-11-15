# Cardboard2: Object Interaction and Scoring System

Este repositorio contiene un sistema interactivo desarrollado en Unity que incluye:
- Teletransportación de objetos.
- Un sistema de puntuación dinámico.
- Comportamiento de un enemigo (zombie) activado por eventos del jugador.

Está diseñado para proyectos de realidad virtual o aumentada, donde los usuarios interactúan con el entorno mediante la mirada o clics.

## Contenido

1. **ObjectController.cs**  
   Controla los objetos interactivos en la escena, permitiendo que reaccionen a eventos del jugador, como mirarlos o seleccionarlos.

2. **PuntuacionManager.cs**  
   Maneja la puntuación del jugador y activa eventos personalizados, como la aparición de un enemigo al alcanzar una puntuación objetivo.

3. **ZombieController.cs**  
   Controla el comportamiento del zombie, quien persigue al jugador al activarse y puede reducir la puntuación si logra alcanzarlo.

---

## Scripts

### 1. ObjectController.cs
Este script maneja los objetos interactivos para proporcionar una experiencia dinámica e inmersiva:
- Cambia entre materiales "activo" e "inactivo" dependiendo de si son observados.
- Teletransporta los objetos a posiciones aleatorias dentro de la escena.
- Integra un sistema de puntuación al otorgar puntos al interactuar con el objeto.

#### Métodos principales:
- `OnPointerEnter()`: Cambia el material al estado activo y otorga puntos.
- `OnPointerExit()`: Cambia el material al estado inactivo.
- `OnPointerClick()`: Teletransporta el objeto a una nueva posición.

---

### 2. PuntuacionManager.cs
Este script administra el sistema de puntuación del jugador, registrando puntos en tiempo real y lanzando eventos cuando se alcanzan metas específicas.

#### Características:
- Incrementa o reduce la puntuación en función de las interacciones.
- Lanza un evento (`OnPuntuacionAlcanzada50`) al alcanzar 50 puntos para activar el zombie.
- Actualiza la UI de puntuación visible para el jugador.

#### Métodos principales:
- `AgregarPuntos(int puntos)`: Incrementa la puntuación y verifica eventos.
- `RestarPuntos(int puntos)`: Reduce la puntuación cuando el jugador sufre daño.
- `ActualizarTextoPuntuacion()`: Actualiza el texto en pantalla.

---

### 3. ZombieController.cs
Este script gestiona el comportamiento de un enemigo (zombie) que persigue al jugador. El zombie se activa al alcanzar una puntuación específica.

#### Características:
- **Movimiento dinámico:** El zombie se dirige hacia el jugador con una velocidad proporcional a la puntuación actual.
- **Interacción:** Si el zombie alcanza al jugador, resta puntos y se teletransporta a su posición inicial.
- **Reacción visual:** Cambia su material cuando el jugador lo observa.

#### Métodos principales:
- `ActivarZombie()`: Activa el zombie y comienza su movimiento.
- `MoverZombie(float velocidad)`: Mueve el zombie hacia el jugador en el plano X-Z.
- `VerificarContacto()`: Detecta si el zombie toca al jugador y resta puntos.
- `OnPointerEnter() / OnPointerExit()`: Cambian el material del zombie según si está siendo observado.
