# JoinNCFiles_DTVM
**Spojovač NC souborů pro DTVM Prostějov**
Aplikace spojí NC soubory vytvořené specifickými postprocesory a pojmenované XXX_d.nc a XXX_d-TOOL.nc do 1 souboru XXX.nc a XXX-TOOL.nc.
Při spojení obsahu jednotlivých souborů dojde k přečíslování řádků. Soubory musí být pojmenovány podle specifikace a musí být
uloženy do stejné složky. Informace o postprocesoru a posledního generovaného NC výstupu jsou čteny ze souboru pamscl.dat příslušné
verze Edgecam. Spojené soubory jsou vytvořeny do stejné složky jako je složka ze které jsou NC soubory čteny.

Pro úspěšné fungování aplikace je nutné mít nainstalovaný Edgecam libovolné verze s platnou licencí, ve stejné složce jako je soubor JoinNCFiles.exe musí být také uložen soubor s nastavením pro daný postprocesor **MC3000SETTINGS.xml**.
