import { useEffect, useState } from 'react';
import { Client, SubscriptionLookupDto } from '../api/api';

const apiClient = new Client(process.env.REACT_APP_SERVER_URL);

function PurchasePage() {
    const [subscriptions, setSubscriptions] = useState<SubscriptionLookupDto[]>([]);
    const [currentSub, setCurrentSub] = useState<SubscriptionLookupDto | null>(null);
    const [currentPeriod, setCurrentPeriod] = useState<number | null>(null);
    const [price, setPrice] = useState<number | null>(null);

    useEffect(() => {
        const getSubs = async () => {
            const subs = await apiClient.getAllSubscriptions('1.0');
            setSubscriptions(subs.subscriptions ?? []);
        };
        getSubs();
    }, []);

    useEffect(() => {
        if (currentSub && currentPeriod) {
            setPrice(currentSub.price! * currentPeriod);
        } else {
            setPrice(null);
        }
    }, [currentSub, currentPeriod]);

    const handleOnClickBuySubscription = () => {
        if (!currentSub || !currentPeriod) {
            alert('Пожалуйста, выберите подписку и период.');
            return;
        }
        alert('Покупка подписок в данный момент недоступна!');
    };

    const handleOnChangeSub = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selected = subscriptions.find(s => s.name === e.target.value) || null;
        setCurrentSub(selected);
    };

    const handleOnChangePeriod = (e: React.ChangeEvent<HTMLInputElement>) => {
        setCurrentPeriod(Number(e.target.value));
    };

    type Period = {
        period: string;
        multiplier: number;
    };

    const periods: Period[] = [
        { period: '1 месяц', multiplier: 1 },
        { period: '3 месяца', multiplier: 3 },
        { period: '6 месяцев', multiplier: 6 },
        { period: '12 месяцев', multiplier: 12 }
    ];

    return (
        <div className='content'>
            <div className='purchase_block text'>

                <div className='purchase_text'>
                    Подписки:
                </div>
                
                <div className='subscription_block'>
                    {subscriptions.map(sub => (
                        <>
                        {sub.price! > 0 ?
                            <label className='subscription' key={sub.id}>
                                <input
                                    className='radio_box'
                                    name='subscription'
                                    type='radio'
                                    value={sub.name}
                                    onChange={handleOnChangeSub}
                                />
                                <div className='text'>«{sub.name}»</div>
                            </label>
                            :
                            <></>
                        }
                        </>
                    ))}
                </div>

                <div className='purchase_text'>
                    Период подписки:
                </div>
                
                <div className='period'>
                    {periods.map(p => (
                        <label className='radio_box_container' key={p.multiplier}>
                            <input
                                className='radio_box'
                                name='period'
                                type='radio'
                                value={p.multiplier}
                                onChange={handleOnChangePeriod}
                            />
                            <div className='text'>{p.period}</div>
                        </label>
                    ))}
                </div>

                <div className='payment_block'>
                    <div className='price_block'>
                        Цена:
                        <div className='price_block'>
                            {price ? `${price} ₽` : '—'}
                        </div>
                    </div>

                    <input
                        className='buy_subscription_button'
                        type='button'
                        value='Купить подписку'
                        onClick={handleOnClickBuySubscription}
                    />
                </div>

            </div>
        </div>
    );
}

export default PurchasePage;