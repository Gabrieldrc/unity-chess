1- Las piezas se reubican en sus posiciones default.

[//]: # (2- Activar los coliders de las piezas del jugador que puede jugar)
3- Juego:
  - El mouse pasa sobre la pantalla.
  - trazar un raycast desde la camara
  - Si el ray colisiona con una pieza: 
    - ilumina la pieza.
  - Si colisiona una pieza y apreta click:
    - la selecciona.
  - Si una pieza esta seleccionada se tiene que:
      - deseleccionar la pieza que estaba antes seleccionada si es que habia
      - iluminar las posibles posiciones
  - Si una pieza es deseleccionada:
    - se debe quitar la iluminacion de las posibles posiciones.
  - Cuando una pieza esta seleccionada y el mouse esta encima de una posible posicion.
	- Se debe iluminar de otro color (para indicar la posicion que se apunta.
	- si se apreta click se debe mover la pieza seleccionada a esa posicion.
  - Si una pieza colisiona con otra:
    - la pieza en movimiento "come" a la pieza que se encontraba previamente en la posicion destino.
  - Si una pieza es "comida":
    - Se debe desactivar su colider
    - Se debe ubicar al lado del tablero del lado del enemigo.
  - Despues de mover, se debe apretar pausa el cronometro del jugador:
    - y se activa automaticamente el del contrincante
  
# Chess Rules
## CHECK
A King is in check, when it is attacked by the opponent’s piece. The King can never be captured.
A King must get out of the check immediately:.
- by moving the King
- by capturing the piece that gave the check
- by blocking the check with one of the pieces of his team. This is impossible if the check was given by the Knight.

## MATE (CHECKMATE)
If the King cannot escape from the check, the position is checkmate and the game is over.
The player who got checkmated gets zero point and the player giving mate gets one point.

## STALEMATE & DRAW
There are three possible results in a chess game. If neither side wins, the game is a draw and both players get half a point. A draw is half as good as a win, but much better, than losing.
The different forms a drawn game are the following:
 - Stalemate: When a player whose turn it is has no legal moves by any of his/her pieces, but is not in check.
 - Perpetual check & three times repetition
 - Theoretical draw (when there are not sufficient pieces on the board to checkmate)
 - Draw agreed by the players (Stalemate occurs when the player,
    who has to make the move, has no possible move, and his King is not in check.

# Notations

White/Black - Name - Score - Time Used

round number - white movement - black movement
hasta 60 rounds

https://sakkpalota.hu/index.php/en/chess/rules#writing
https://app.diagrams.net/?src=about#G1poXtjMgPGbnrlz1cBOBbESmlAo43a1ba