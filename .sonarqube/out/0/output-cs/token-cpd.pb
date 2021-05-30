»
;D:\RobDev\RogedoLife\Rogedo.LifeEngine.Interfaces\IArena.cs
	namespace 	
Rogedo
 
. 

LifeEngine 
. 

Interfaces &
{ 
public 

	interface 
IArena 
{ 
List 
< 
ICell 
> 

ArenaCells 
{  
get! $
;$ %
}& '
void 

Initialise 
( 
int 
	dimension %
)% &
;& '
int		 
GetArenaSize		 
(		 
)		 
;		 
string

 
GetSignature

 
(

 
)

 
;

 
void 
Seed 
( 
int 
x 
, 
int 
y 
) 
;  
ICell 
	GetCellAt 
( 
int 
x 
, 
int "
y# $
)$ %
;% &
void 
MakeNextGeneration 
(  
)  !
;! "
int 
GetPopulation 
( 
) 
; 
int 
GetGeneration 
( 
) 
; 
bool 
	Repeating 
{ 
get 
; 
} 
void 
InitialiseRandomly 
(  
int  #
	dimension$ -
)- .
;. /
} 
} ©
:D:\RobDev\RogedoLife\Rogedo.LifeEngine.Interfaces\ICell.cs
	namespace 	
Rogedo
 
. 

LifeEngine 
. 

Interfaces &
{ 
public 

	interface 
ICell 
{ 
CellGeneration 

Generation !
{" #
get$ '
;' (
}) *
void 
SetGeneration 
( 
CellGeneration )

generation* 4
)4 5
;5 6
}		 
}

 µ
ID:\RobDev\RogedoLife\Rogedo.LifeEngine.Interfaces\Types\CellGeneration.cs
	namespace 	
Rogedo
 
. 

LifeEngine 
. 

Interfaces &
.& '
Types' ,
{ 
public 

enum 
CellGeneration 
{ 
Dead 
, 
Current 
, 
Next 
} 
}		 