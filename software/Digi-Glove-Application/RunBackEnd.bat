set arg1=%~dp0

cd "%arg1%VirtualEnv\Scripts"
activate & python "%arg1%BackEnd\python101.py"