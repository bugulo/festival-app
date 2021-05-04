# Seminář C# - ICS Projekt - Aplikácia na správu festivalu
Cielom našej aplikácie bolo vytvoriť jednoduchú a ľahko rozširiteľnú aplikáciu na správu festivalu. Implementujeme v nej 3 okná Program, Stage a Band. V jednotlivých oknách môžme kliknutím na tlačidlo: Add new (item) môžme pridať jednotlivé Bands, Stages, a Sloty vypísaním potrebných údajov a následným stlačením tlačidla Save. V okne Program si môžte naviac vybrať jeden z uložených Stagov, pre ktoré sa vypíšu naň pridané sloty. V prípade,že si nevyberieme žiaden Stage, vypíšu sa všetky Sloty.

## Na projekte pracovali :
&nbsp;
|         Meno a Priezvisko              |  Login       | 
| ---------------------------------------|--------------| 
| Šlesár Michal                          | 	xslesa01    | 
| Jurkechová Adriana	                 |   xjurke02   | 
| Mikuš Michal                           | 	xmikus18    |
| Lecső René                             | 	xlecso00    |
| Seipel Richard                         | 	xseipe00    |

## Ošetrenie časových slotov
Zadávame počiatočný čas vystúpenia a koncový čas vystúpenia a pri pridávaní časového slotu do databázy kontrolujeme fukciou isSlotAvaiable kde sa kontroluje podľa začiatku a konca časového intervalu či sa náhodou v databáze nenachádza iný Slot v danom časovom intervale. V prípade že nenastane žiadny konflikt sa Slot do databázy pridá ak konflikt nastane vypíšeme chybovú hlášku o kolízii v aplikácii.
