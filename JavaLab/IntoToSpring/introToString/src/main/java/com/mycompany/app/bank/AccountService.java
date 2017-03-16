package com.mycompany.app.bank;

/**
 * Created by igor-z on 16-Mar-17.
 */
public interface AccountService {
    public void createAccount(String accountNo);
    public void removeAccount(String accountNo);
    public void deposit(String accountNo, double amount);
    public void withdraw(String accountNo, double amount) throws InsufficientBalanceException;
    public double getBalance(String accountNo);
}
