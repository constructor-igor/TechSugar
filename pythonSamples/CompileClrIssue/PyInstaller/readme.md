https://github.com/pyinstaller/pyinstaller/issues/1801

install pyinstaller: ```install git+https://github.com/pyinstaller/pyinstaller```

compile: ```pyinstaller check-clr.py --hidden-import=clr```

start: ```dist\check-clr\check-clr.exe```

compilation output:
```
130 INFO: PyInstaller: 3.3.dev0+g483c819
130 INFO: Python: 3.5.2
131 INFO: Platform: Windows-7-6.1.7601-SP1
132 INFO: wrote D:\My\@gh\TechSugar\pythonSamples\CompileClrIssue\PyInstaller\check-clr.spec
134 INFO: UPX is not available.
136 INFO: Extending PYTHONPATH with paths
['D:\\My\\@gh\\TechSugar\\pythonSamples\\CompileClrIssue\\PyInstaller',
 'D:\\My\\@gh\\TechSugar\\pythonSamples\\CompileClrIssue\\PyInstaller']
136 INFO: checking Analysis
136 INFO: Building Analysis because out00-Analysis.toc is non existent
137 INFO: Initializing module dependency graph...
141 INFO: Initializing module graph hooks...
143 INFO: Analyzing base_library.zip ...
3640 INFO: Analyzing hidden import 'clr'
3640 INFO: running Analysis out00-Analysis.toc
4131 INFO: Caching module hooks...
4136 INFO: Analyzing D:\My\@gh\TechSugar\pythonSamples\CompileClrIssue\PyInstaller\check-clr.py
4140 INFO: Loading module hooks...
4140 INFO: Loading module hook "hook-encodings.py"...
4258 INFO: Loading module hook "hook-clr.py"...
4261 INFO: Loading module hook "hook-xml.py"...
4534 INFO: Loading module hook "hook-pydoc.py"...
4553 INFO: Looking for ctypes DLLs
4559 INFO: Analyzing run-time hooks ...
4566 INFO: Looking for dynamic libraries
4703 INFO: Looking for eggs
4704 INFO: Using Python library C:\Users\igor-z\AppData\Local\Continuum\Anaconda3\python35.dll
4704 INFO: Found binding redirects:[]
4710 INFO: Warnings written to D:\My\@gh\TechSugar\pythonSamples\CompileClrIssue\PyInstaller\build\check-clr\warncheck-clr.txt
4720 INFO: checking PYZ
4720 INFO: Building PYZ because out00-PYZ.toc is non existent
4720 INFO: Building PYZ (ZlibArchive) D:\My\@gh\TechSugar\pythonSamples\CompileClrIssue\PyInstaller\build\check-clr\out00-PYZ.pyz
5300 INFO: checking PKG
5301 INFO: Building PKG because out00-PKG.toc is non existent
5301 INFO: Building PKG (CArchive) out00-PKG.pkg
5318 INFO: Bootloader C:\Users\igor-z\AppData\Local\Continuum\Anaconda3\lib\site-packages\PyInstaller\bootloader\Windows-64bit\run.exe
5319 INFO: checking EXE
5319 INFO: Building EXE because out00-EXE.toc is non existent
5319 INFO: Building EXE from out00-EXE.toc
5320 INFO: Appending archive to EXE D:\My\@gh\TechSugar\pythonSamples\CompileClrIssue\PyInstaller\build\check-clr\check-clr.exe
5324 INFO: checking COLLECT
5325 INFO: Building COLLECT because out00-COLLECT.toc is non existent
5325 INFO: Building COLLECT out00-COLLECT.toc
```
