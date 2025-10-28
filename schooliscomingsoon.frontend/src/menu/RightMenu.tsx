import { useContext, useEffect, useState } from 'react';
import { Client, CurrentSubscriptionVm, SubscriptionLookupDto } from '../api/api';
import { AuthContext } from '../auth/auth-provider';

const apiClient = new Client(process.env.REACT_APP_SERVER_URL);

function RightMenu() {
    const [subscriptions, setSubscriptions] = useState<SubscriptionLookupDto[]>([]);
    const [currentSub, setCurrentSub] = useState<CurrentSubscriptionVm | null>(null);
    const { isAuthenticated } = useContext(AuthContext);

    useEffect(() => {
        const getSubs = async () => {
            const subs = await apiClient.getAllSubscriptions('1.0');
            setSubscriptions(subs.subscriptions ?? []);
        };
        getSubs();
    }, []);

    useEffect(() => {
        const getCurrentSub = async () => {
            if (isAuthenticated) {
                const curSub = await apiClient.getCurrentSubscription('1.0');
                setCurrentSub(curSub);
            } else {
                setCurrentSub(null);
            }
        };
        getCurrentSub();
    }, [isAuthenticated]);

    const handleOnClickBuySubscription = () => {
        window.location.href = '/purchase';
    };

    return (
        <div className="right_menu">
            <div>Подписки:</div>

            <div className="subs_list">
                {subscriptions
                    .filter(sub => sub.lvl !== 0)
                    .map(sub => (
                        <div className="sub" key={sub.id}>
                            «{sub.name}» — {sub.price} руб.
                        </div>
                    ))}
            </div>

            {isAuthenticated && currentSub && (
                <>
                    <div className="splitter" />
                    <div className="current_sub">
                        <div>Текущая подписка:</div>
                        <div className="sub">
                            «{currentSub.name}»<br />
                            <br />
                            Осталось до окончания подписки: {currentSub.expiresAfter}
                        </div>
                    </div>
                    <input className="buy_subscription_button" type="button" value="Купить подписку" onClick={handleOnClickBuySubscription}></input>
                </>
            )}
        </div>
    );
}

export default RightMenu;