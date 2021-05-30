©'
5D:\RobDev\RogedoLife\Rogedo.LifeEngine.Rig\Program.cs
	namespace 	
Rogedo
 
. 

LifeEngine 
. 
Rig 
{ 
class 	
Program
 
{ 
static		 
void		 
Main		 
(		 
)		 
{

 	
const 
int 
	dimension 
=  !
$num" $
;$ %
IArena 
	gameArena 
= 
new "
Arena# (
(( )
)) *
;* +
	gameArena 
. 
InitialiseRandomly (
(( )
	dimension) 2
)2 3
;3 4
Console 
. 
Clear 
( 
) 
; 
Console 
. 
CursorVisible !
=" #
false$ )
;) *
while 
( 
	gameArena 
. 
GetPopulation *
(* +
)+ ,
>- .
$num/ 0
&&1 3
!4 5
	gameArena5 >
.> ?
	Repeating? H
)H I
{ 

PrintArena 
( 
	gameArena $
,$ %
	dimension& /
)/ 0
;0 1
	gameArena 
. 
MakeNextGeneration ,
(, -
)- .
;. /
}   
Console!! 
.!! 
CursorVisible!! !
=!!" #
true!!$ (
;!!( )
}"" 	
static$$ 
void$$ 

PrintArena$$ 
($$ 
IArena$$ %
arena$$& +
,$$+ ,
int$$- 0
	dimension$$1 :
)$$: ;
{%% 	
ConsoleColor&& 
defaultColour&& &
=&&' (
Console&&) 0
.&&0 1
ForegroundColor&&1 @
;&&@ A
int(( 
currentCell(( 
=(( 
$num(( 
;((  
int)) 
x)) 
;)) 
int** 
y** 
;** 
foreach++ 
(++ 
var++ 
c++ 
in++ 
arena++ #
.++# $

ArenaCells++$ .
)++. /
{,, 
x== 
=== 
(== 
currentCell==  
%==! "
	dimension==# ,
)==, -
*==. /
$num==0 1
;==1 2
y>> 
=>> 
currentCell>> 
/>>  !
	dimension>>" +
;>>+ ,
Console?? 
.?? 
SetCursorPosition?? )
(??) *
x??* +
,??+ ,
y??- .
)??. /
;??/ 0
if@@ 
(@@ 
c@@ 
.@@ 

Generation@@  
==@@! #

Interfaces@@$ .
.@@. /
Types@@/ 4
.@@4 5
CellGeneration@@5 C
.@@C D
Dead@@D H
)@@H I
{AA 
ConsoleBB 
.BB 
ForegroundColorBB +
=BB, -
defaultColourBB. ;
;BB; <
ConsoleCC 
.CC 
WriteCC !
(CC! "
$strCC" &
)CC& '
;CC' (
}DD 
elseEE 
{FF 
ConsoleGG 
.GG 
ForegroundColorGG +
=GG, -
ConsoleColorGG. :
.GG: ;
YellowGG; A
;GGA B
ConsoleHH 
.HH 
WriteHH !
(HH! "
$strHH" &
)HH& '
;HH' (
ConsoleII 
.II 
ForegroundColorII +
=II, -
defaultColourII. ;
;II; <
}JJ 
currentCellKK 
++KK 
;KK 
}MM 
ConsoleOO 
.OO 
ForegroundColorOO #
=OO$ %
ConsoleColorOO& 2
.OO2 3
WhiteOO3 8
;OO8 9
ConsolePP 
.PP 
SetCursorPositionPP %
(PP% &
$numPP& '
,PP' (
	dimensionPP) 2
)PP3 4
;PP4 5
ConsoleQQ 
.QQ 
	WriteLineQQ 
(QQ 
$"QQ  
$strQQ  ,
{QQ, -
arenaQQ- 2
.QQ2 3
GetGenerationQQ3 @
(QQ@ A
)QQA B
}QQB C
"QQC D
)QQD E
;QQE F
ConsoleRR 
.RR 
SetCursorPositionRR %
(RR% &
$numRR& '
,RR' (
	dimensionRR) 2
+RR3 4
$numRR5 6
)RR6 7
;RR7 8
ConsoleSS 
.SS 
	WriteLineSS 
(SS 
$"SS  
$strSS  ,
{SS, -
arenaSS- 2
.SS2 3
GetPopulationSS3 @
(SS@ A
)SSA B
}SSB C
$strSSC L
"SSL M
)SSM N
;SSN O
ConsoleTT 
.TT 
ForegroundColorTT #
=TT$ %
defaultColourTT& 3
;TT3 4
}UU 	
}WW 
}XX 