import { useContext, useEffect, useState } from 'react';
import { Client, CurrentSubscriptionVm, SubscriptionLookupDto } from '../api/api'; 
import { AuthContext } from '../auth/auth-provider';

const apiClient = new Client('https://localhost:44399');

function RightMenu() {
    const [subscriptions, setSubscriptions] = useState<SubscriptionLookupDto[]>([]);
    const [currentSub, setCurrentSub] = useState<CurrentSubscriptionVm>();

    const { id, isAuthenticated } = useContext(AuthContext);

    useEffect(() => {
        async function getSubs() {
            let subs = await apiClient.getAllSubscriptions('1.0');
            setSubscriptions(subs.subscriptions!);
        }
    
        getSubs();
    }, []);

    useEffect(() => {
        async function getCurrentSub() {
            if (isAuthenticated)
            {
                let curSub = await apiClient.getCurrentSubscription(id, '1.0');
                setCurrentSub(curSub);
            }
        }
        
        getCurrentSub();
    }, [isAuthenticated]);

    return (
        <div className='right_menu'>
            Подписки:
            <div className='subs_list'>
                <>
                    {subscriptions?.map((sub) => (
                        <>
                            {sub.lvl == 0
                            ?
                                <></>
                            :
                                <div className='sub'>
                                    «{sub.name}» - {sub.price} руб.
                                </div>
                            }
                            
                        </>
                    ))}
                </>
            </div>
            
            {isAuthenticated
            ?
                <>
                    <div className="splitter"/>
            
                    <div className='current_sub'>
                        Текущая подписка:
                        <div className='sub'>
                            «{currentSub?.name}»
                            <br></br>
                            <br></br>
                            Осталось до окончания подписки: {currentSub?.expiresAfter}
                        </div>
                    </div>
                </>
            :
                <></>
            }
            
            
        </div>
    );
};
export default RightMenu;