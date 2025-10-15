import { useEffect, useState } from "react";
import { Client, SubscriptionLookupDto } from "../../api/api";

const apiClient = new Client(process.env.REACT_APP_SERVER_URL);

export default function PostEditorToggleSwitchPanel(props: any) {
    const [firstLVLSubChecked, setFirstLVLSubChecked] = useState(false);
    const [secondLVLSubChecked, setSecondLVLSubChecked] = useState(false);
    const [thirdLVLSubChecked, setThirdLVLSubChecked] = useState(false);
    const [subscriptions, setSubscriptions] = useState<SubscriptionLookupDto[]>([]);

    const onChangeSub = () => {
        if (subscriptions.length > 0) {
            if (firstLVLSubChecked) {
                props.setSubscriptionId(subscriptions[1].id);
            } else if (secondLVLSubChecked) {
                props.setSubscriptionId(subscriptions[2].id);
            } else if (thirdLVLSubChecked) {
                props.setSubscriptionId(subscriptions[3].id);
            }
            else {
                props.setSubscriptionId(subscriptions[0].id);
            }
        }
    }

    const handleOnSwitchFirstLVLSub = () => {
        setFirstLVLSubChecked(!firstLVLSubChecked);
        setSecondLVLSubChecked(false);
        setThirdLVLSubChecked(false);
    }

    const handleOnSwitchSecondLVLSub = () => {
        setSecondLVLSubChecked(!secondLVLSubChecked);
        setFirstLVLSubChecked(false);
        setThirdLVLSubChecked(false);
    }

    const handleOnSwitchThirdLVLSub = () => {
        setThirdLVLSubChecked(!thirdLVLSubChecked);
        setFirstLVLSubChecked(false);
        setSecondLVLSubChecked(false);
    }

    useEffect(() => {
        async function getSubs() {
            let subs = await apiClient.getAllSubscriptions('1.0');
            setSubscriptions(subs.subscriptions!);
            props.setSubscriptionId(subs.subscriptions![0].id);
        }

        getSubs();
    }, []);

    useEffect(() => {
        onChangeSub();
    }, [firstLVLSubChecked, secondLVLSubChecked, thirdLVLSubChecked]);
    
    useEffect(() => {  
        if (props.subscriptions.length > 0) {
            if (props.subscriptions[1].id === props.subscriptionId) {
                setFirstLVLSubChecked(true);
            }
            if (props.subscriptions[2].id === props.subscriptionId) {
                setSecondLVLSubChecked(true);
            }
            if (props.subscriptions[3].id === props.subscriptionId) {
                setThirdLVLSubChecked(true);
            }
        }
        
    }, [props.subscriptions]);
    
    return (
        <div className='switch_buttons'>

            <div className='toggle_switch_with_text'>
                <label className='toggle-switch'>
                    <input
                        type='checkbox'
                        id='firstLVL'
                        className='toggle-input'
                        onChange={handleOnSwitchFirstLVLSub}
                        checked={firstLVLSubChecked}
                    />

                    <span className='slider round'></span>
                </label>
                <div className='toggle_switch_text'>
                    {subscriptions.length > 0 ? subscriptions[1].name : ''}
                </div>
            </div>

            <div className='toggle_switch_with_text'>
                <label className='toggle-switch'>
                    <input
                        type='checkbox'
                        id='secondLVL'
                        className='toggle-input'
                        onChange={handleOnSwitchSecondLVLSub}
                        checked={secondLVLSubChecked}
                    />

                    <span className='slider round'></span>
                </label>
                <div className='toggle_switch_text'>
                    {subscriptions.length > 0 ? subscriptions[2].name : ''}
                </div>
            </div>

            <div className='toggle_switch_with_text'>
                <label className='toggle-switch'>
                    <input
                        type='checkbox'
                        id='thirdLVL'
                        className='toggle-input'
                        onChange={handleOnSwitchThirdLVLSub}
                        checked={thirdLVLSubChecked}
                    />

                    <span className='slider round'></span>
                </label>
                <div className='toggle_switch_text'>
                    {subscriptions.length > 0 ? subscriptions[3].name : ''}
                </div>
            </div>

        </div>
    );
};