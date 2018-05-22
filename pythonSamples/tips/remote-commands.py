
# 
# how to execute command on remove windows machine
# https://stackoverflow.com/questions/18961213/how-to-connect-to-a-remote-windows-machine-to-execute-commands
# 
def remove_wmi():
    print("[remove_wmi] started")
    import wmi
    c=wmi.WMI('machine name',user='username',password='password')
    process_id, return_value = c.Win32_Process.Create(CommandLine="cmd.exe /c dir>")
    print("[remove_wmi] finished, result={0}".format(result))

def remove_winrm():
    print("[remove_winrm] started")
    import winrm
    # Create winrm connection.
    sess = winrm.Session('https://10.0.0.1', auth=('username', 'password'), transport='kerberos')
    result = sess.run_cmd('ipconfig', ['/all'])
    print("[remove_winrm] finished, result={0}", result)

def remove_winrm_powershell():
    print("[remove_winrm_powershell] started")
    import winrm
    ps_script = """$strComputer = $Host 
Clear 
$RAM = WmiObject Win32_ComputerSystem
$MB = 1048576

"Installed Memory: " + [int]($RAM.TotalPhysicalMemory /$MB) + " MB" """

    s = winrm.Session('windows-host.example.com', auth=('john.smith', 'secret'))
    r = s.run_ps(ps_script)
    print("[remove_winrm_powershell] finished, result={0}",format(r.std_out))

if __name__ == "__main__":
    remove_wmi()
    remove_winrm()
    remove_winrm_powershell()