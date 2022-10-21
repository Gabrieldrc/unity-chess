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

# TODO
## Pawn canmove
The king method can move mark that can not move also when a pawn enemy move forward.
That is wrong. I should be allow to move, but the pawn does not has a difference between
a normal move and a capture movement.
## Pawn can do EN PASSANT
if a pawn is taking its right to do the first move with 2 steps AND already a enemy pawn
was next to the destination point of that pawn. The enemy pawn ONLY in its turn NEXT to
that movement, could do EN PASSANT where it move diagonally as if the first pawn would
has move one step, and "eat" it.
https://www.youtube.com/watch?v=1q7lZilVy04
## State of Check
When a player move a piece:
    the game has to check if the next player is in check.
        How? check if the piece that was move right before can eat the king of the next player in its next turn.
if it is,
# DONE
## King allow movement
The king only can move one step but only where:
 - is empty
 - is an opponent
 - is NOT on check
