package com.mycompany.app.bank;

/**
 * Created by igor-z on 16-Mar-17.
 */
public interface AccountDao {
    public void createAccount(Account account);
    public void updateAccount(Account account);
    public void removeAccount(Account account);
    public Account findAccount(String accountNo);
}